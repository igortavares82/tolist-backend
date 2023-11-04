using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Commands.ConfigurationCommands;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Application
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly IConfigurationRepository _repository;

        public UpdateCommandHandler(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<Unit>(Unit.Value);
            var configuration = await _repository.GetOneAsync(it => it.Id == request.ResourceId);

            if (configuration == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Configuration"));

            var workers = request.Workers.Select(it => new Worker(it.Type, it.IsEnabled, it.Delay, null, it.Properties)).ToArray();
            var logger = new Logger(request.Logger.LevelConfiguration);

            configuration.Update(request.Name, workers, logger);

            await _repository.UpdateAsync(it => it.Id == configuration.Id, configuration);
            return result.SetResult(ResultStatus.NoContent, null);
        }
    }
}
