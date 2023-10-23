using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.ConfigurationCommands;

namespace Standard.ToList.Api.Controllers
{
    [Route("configurations")]
    public class ConfigurationController : Controller
    {
        private readonly IMediator _mediator;

        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);
    }
}

