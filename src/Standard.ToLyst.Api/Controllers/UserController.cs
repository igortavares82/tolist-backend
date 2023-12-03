using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToLyst.Application.Commands.UserCommands;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Api.Controllers
{
    [Route("users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserQuery _userQuery;

        public UserController(IMediator mediator, IUserQuery userQuery)
        {
            _mediator = mediator;
            _userQuery = userQuery;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromBody] UpdateCommand request) => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteCommand request) => await _mediator.Send(request);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Request request) => await _userQuery.GetAsync(request);
    }
}

