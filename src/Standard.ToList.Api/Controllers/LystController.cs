using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.LystCommands;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Api.Controllers
{
    [Route("lysts")]
    public class LystController : Controller
    {
        private readonly IMediator _mediator;

        public LystController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request)
        {
            var result = await _mediator.Send(request);
            return new CreatedResult($"/lysts/{result.Data.Id}", result);
        }
    }
}

