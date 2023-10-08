using System.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.MarketCommands;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Api.Controllers
{
    [Route("markets")]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);
    }
}

