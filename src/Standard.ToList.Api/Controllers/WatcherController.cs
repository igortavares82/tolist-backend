using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.WatcherCommands;

namespace Standard.ToList.Api.Controllers
{
	[Route("watches")]
    [Authorize(Roles = "Admin,Premium")]
	public class WatcherController : Controller
	{
		private readonly IMediator _mediator;

        public WatcherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);
	}
}

