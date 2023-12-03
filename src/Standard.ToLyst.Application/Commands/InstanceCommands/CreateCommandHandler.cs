using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;
using Standard.ToLyst.Model.ViewModels.Lysts;

namespace Standard.ToLyst.Application.Commands.InstanceCommands
{
	public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<InstanceViewModel>>
	{
        private readonly ILystRepository _lystRepository;
        private readonly IMarketRepository _marketRepository;

        public CreateCommandHandler(ILystRepository lystRepository, IMarketRepository marketRepository)
        {
            _lystRepository = lystRepository;
            _marketRepository = marketRepository;
        }

        public async Task<Result<InstanceViewModel>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<InstanceViewModel>(null);
            var lyst = await _lystRepository.GetOneAsync(request.Query());

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            var instance = lyst.CreateInstance(request.Name);
            var markets = await _marketRepository.GetAsync(it => it.IsEnabled == true);
            await _lystRepository.UpdateAsync(it => it.Id == lyst.Id, lyst);

            return result.SetResult(new InstanceViewModel(instance, markets.ToArray()),
                                    ResultStatus.Created,
                                    Messages.CreatedSuccess.SetMessageValues("List copy"));
        }
    }
}

