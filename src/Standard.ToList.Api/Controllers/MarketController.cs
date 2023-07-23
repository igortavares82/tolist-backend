using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.MarketCommands;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Requests;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Api.Controllers
{
    [Route("markets")]
    public class MarketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductQuery _query;

        public MarketController(IMediator mediator, IProductQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request)
        {
            var result = await _mediator.Send(request);
            return Created($"/markets/{result.Data.Id}", result);
        }
    }
}

