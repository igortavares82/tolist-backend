using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.WatcherCommands;
using Standard.ToList.Model.Aggregates.Watchers;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Watchers;

namespace Standard.ToList.Api.Controllers
{
	[Route("watches")]
    [Authorize(Roles = "Admin,Premium")]
	public class WatcherController : Controller
	{
		private readonly IMediator _mediator;
        private readonly IWatcherQuery _query;

        public WatcherController(IMediator mediator, IWatcherQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] UpdateCommand request) => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteCommand request) => await _mediator.Send(request);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Request request) => await _query.GetAsync(request);

        [HttpGet]
        public async Task<IActionResult> Get(WatcherRequest request) => await _query.GetAsync(request);
    }
}

