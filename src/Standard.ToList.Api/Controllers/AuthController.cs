using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.AuthCommands;
using Standard.ToList.Model.Common;

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
        public async Task<IActionResult> Token([FromBody] AuthCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Status == ResultStatus.NotFound)
                return NotFound(result);

            return Ok(result);
        }
    }
}

