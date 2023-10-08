using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.InstanceCommands;
using Standard.ToList.Model.Aggregates.Lysts;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Api.Controllers
{
    [Authorize]
    [Route("instances")]
    public class InstanceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IInstanceQuery _instanceQuery;

        public InstanceController(IMediator mediator, IInstanceQuery instanceQuery)
        {
            _mediator = mediator;
            _instanceQuery = instanceQuery;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpDelete("{id}/{instanceId}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteCommand request) => await _mediator.Send(request);

        [HttpPatch("{id}/{instanceId}")]
        public async Task<IActionResult> Patch([FromBody] UpdateCommand request) => await _mediator.Send(request);

        [HttpPatch("{id}/{instanceId}/{productId}/{value}")]
        public async Task<IActionResult> Check([FromRoute] CheckCommand request) => await _mediator.Send(request);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(InstanceRequest request) => await _instanceQuery.GetAsync(request);
    }
}
