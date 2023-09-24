using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.InstanceCommands;
using Standard.ToList.Model.Aggregates.Lists;

namespace Standard.ToList.Api.Controllers
{
    [Authorize]
    [Route("instances")]
    public class InstanceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILystQuery _lystQuery;

        public InstanceController(IMediator mediator, ILystQuery lystQuery)
        {
            _mediator = mediator;
            _lystQuery = lystQuery;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpDelete("{id}/{instanceId}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteCommand request) => await _mediator.Send(request);

        [HttpPatch("{id}/{instanceId}/{productId}/{value}")]
        public async Task<IActionResult> Check([FromRoute] CheckCommand request) => await _mediator.Send(request);
    }
}
