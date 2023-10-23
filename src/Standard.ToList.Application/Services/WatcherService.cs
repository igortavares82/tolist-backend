using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Options;
using Standard.ToList.Model.ValueObjects;
using MongoDB.Driver.Linq;

namespace Standard.ToList.Application.Services
{
    public class WatcherService : IWatcherService
    {
		private readonly IWatcherRepository _watcherWepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppSettingOptions _settings;
        private readonly SmtpService _smtpService;

        public WatcherService(IWatcherRepository watcherWepository,
                              IUserRepository userRepository,
                              IProductRepository productRepository,
                              IOptions<AppSettingOptions> settings,
                              SmtpService smtpService)
        {
            _watcherWepository = watcherWepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _settings = settings.Value;
            _smtpService = smtpService;
        }

        public async Task SendMessagesAsync()
		{
            var interval = _settings.Workers.WatcherWorker.MessageInterval;
            var watchers = await _watcherWepository.GetAsync(interval);
            var groupedWatchers = watchers?.GroupBy(it => it.UserId).ToList();
            var userIds = groupedWatchers.Select(it => it.Key).ToList();
            var users = _userRepository.GetAsync(it => userIds.Contains(it.Id)).Result.ToList();

            foreach (var watcher in groupedWatchers)
            {
                var message = GenerateMessage(users.First(it => it.Id == watcher.Key), watcher);
                _smtpService.Send(message);
            }

            watchers = groupedWatchers.SelectMany(it => it).ToList();
            watchers.ToList().ForEach(it => it.LastSentMessageDate = DateTime.UtcNow);

            if (watchers.Any())
                await _watcherWepository.UpdateAsync(groupedWatchers.SelectMany(it => it).ToArray());
        }

        public async Task UpdateWatchersAsync()
        {
            var products = _productRepository.GetAsync(it => it.IsEnabled == true &&
                                                             (it.LastUpdate >= DateTime.UtcNow.AddDays(-2) && it.LastUpdate <= DateTime.UtcNow))
                                             .Result
                                             .ToList();

            var productIds = products.Select(_it => _it.Id).ToList();

            var watchers = _watcherWepository.GetAsync(it => it.IsEnabled == true &&
                                                             productIds.Contains(it.ProductId))
                                             .Result
                                             .ToArray();

            foreach (var watcher in watchers)
            {
                try
                {

                    var product = products.First(it => it.Id == watcher.Id);
                    watcher.Update(product.Price);
                }
                catch (Exception ex)
                {
                    // TODO: log here.
                }
            }

            await _watcherWepository.UpdateAsync(watchers);
        }

        private SmtpMessageValueObject GenerateMessage(User user, IGrouping<string, Watcher> watchers)
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}MailMessages/WatcherMessage.html";
            using var reader = new StreamReader(path);
            var template = reader.ReadToEnd();
            var body = string.Join(";", watchers.Select(it => $"<tr><td>{it.Name}</td><td>{it.Current}</td></tr>").ToArray());

            return new SmtpMessageValueObject(user.Email, "ToLyst - Watcher daily!!!", template.Replace("#body#", body));
        }
	}
}

