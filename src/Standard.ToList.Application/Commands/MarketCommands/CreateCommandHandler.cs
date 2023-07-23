﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Application.Commands.MarketCommands
{
	public class CreateCommandHandler : IRequestHandler<CreateCommand, Result<Market>>
	{
        private readonly IMarketRepository _repository;

		public CreateCommandHandler(IMarketRepository repository)
		{
            _repository = repository;
		}

        public async Task<Result<Market>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetOneAsync(it => it.Name == request.Name);

            if (entity != null)
                return new Result<Market>(null, ResultStatus.Exists, "Market already existis.");
            
            entity = await _repository.CreateAsync(new Market(null, request.Name, request.Type, request.BaseUrl));
            return new Result<Market>(entity, ResultStatus.Success, "Market created successfully.");
        }
    }
}

