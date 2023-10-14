using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Options;
using Standard.ToList.Model.ValueObjects;

namespace Standard.ToList.Application.Services
{
    public class WatcherService : IWatcherService
    {
		private readonly IWatcherRepository _watcherWepository;
        private readonly IUserRepository _userRepository;
        private readonly AppSettingOptions _settings;
        private readonly SmtpService _smtpService;

        public WatcherService(IWatcherRepository watcherWepository,
                              IUserRepository userRepository,
                              IOptions<AppSettingOptions> settings,
                              SmtpService smtpService)
        {
            _watcherWepository = watcherWepository;
            _userRepository = userRepository;
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
        }

        private SmtpMessageValueObject GenerateMessage(User user, IGrouping<string, Watcher> watchers)
        {
            using var reader = new StreamReader("");
            var template = reader.ReadToEnd();
            var body = string.Join(";", watchers.Select(it => $"<tr><td>{it.Name}</td><td>{it.Current}</td></tr>").ToArray());

            return new SmtpMessageValueObject(user.Email, "ToLyst - Watcher daily!!!", template.Replace("#body#", body));
        }
	}
}

