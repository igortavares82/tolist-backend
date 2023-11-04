using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Configuration;

namespace Standard.ToList.Application.Commands.ConfigurationCommands
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<ConfigurationViewModel>>
    {
        private readonly IConfigurationRepository _repository;

        public CreateCommandHandler(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ConfigurationViewModel>> Handle(CreateCommand request, CancellationToken cancelationToken)
        {
            var result = new Result<ConfigurationViewModel>(null);
            var config = await _repository.GetAsync(it => it.IsEnabled != false);

            if (config?.Count() > 0)
                return result.SetResult(ResultStatus.UnprosseableEntity, Messages.Exists.SetMessageValues("A configuration"));

            var workers = request.Workers
                                 .Select(it => new Worker(it.Type, it.Delay, null, it.Properties))
                                 .ToArray();

            var logger = new Logger(request.Logger.LevelConfiguration);
            var configuration = new Configuration(request.Name, workers, logger);
            
            await _repository.CreateAsync(configuration);

            return result.SetResult(new ConfigurationViewModel(configuration),
                                    ResultStatus.Created,
                                    Messages.CreatedSuccess.SetMessageValues("Configuration"));
        }
    }
}