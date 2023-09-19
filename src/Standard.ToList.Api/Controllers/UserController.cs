using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.UserCommands;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Api.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Status == ResultStatus.Exists)
            {
                return BadRequest(result);
            }

            return Created($"/users/{result.Data.Id}", result);
        }
    }
}

