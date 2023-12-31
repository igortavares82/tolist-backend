﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToLyst.Application;
using Standard.ToLyst.Application.Commands.AuthCommands;

namespace Standard.ToLyst.Api.Controllers
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

        [HttpPatch("retrieve")]
        public async Task<IActionResult> Retrieve([FromBody] RetrieveCommand request) => await _mediator.Send(request);

        [HttpPatch("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request) => await _mediator.Send(request);
    }
}
