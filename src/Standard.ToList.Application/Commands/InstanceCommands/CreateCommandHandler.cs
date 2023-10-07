using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Application.Commands.InstanceCommands
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
            var lyst = await _lystRepository.GetOneAsync(_lystRepository.Default(request));

            if (lyst == null)
                return result.SetResult(ResultStatus.NotFound, Messages.NotFound.SetMessageValues("Lyst"));

            var instance = lyst.CreateInstance(request.Name);
            var markets = await _marketRepository.GetAsync(it => it.IsEnabled == true);
            await _lystRepository.UpdateAsync(it => it.Id == lyst.Id, lyst);

            return result.SetResult(new InstanceViewModel(instance, markets.ToArray()),
                                    ResultStatus.Created,
                                    Messages.CreatedSucces.SetMessageValues("List copy"));
        }
    }
}

