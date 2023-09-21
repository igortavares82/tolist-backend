using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.AuthCommands;

namespace Standard.ToList.Api.Controllers
{
    [Route("authentication")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] AuthCommand request) => await _mediator.Send(request);

        [HttpPatch("activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateCommand request) => await _mediator.Send(request);
    }
}
