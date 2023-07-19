using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.MarketCommands
{
	public class MarkeCommandHandler : IRequestHandler<MarketCommand, Result<Market>>
	{
        private readonly IMarketRepository _repository;

		public MarkeCommandHandler()
		{
            //_repository = repository;
		}

        public async Task<Result<Market>> Handle(MarketCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByNameAsync(request.Name);

            if (entity != null)
                return new Result<Market>(null, ResultStatus.Exists, "Market already existis.");

            
            entity = await _repository.CreateAsync(new Market(request.Name, request.BaseUrl));

            throw new NotImplementedException();
        }
    }
}

