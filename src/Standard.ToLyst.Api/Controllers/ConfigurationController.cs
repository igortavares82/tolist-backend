using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToLyst.Application.Commands.ConfigurationCommands;
using Standard.ToLyst.Model;
using Standard.ToLyst.Model.Aggregates.Configuration;

namespace Standard.ToLyst.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("configurations")]
    public class ConfigurationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfigurationQuery _query;

        public ConfigurationController(IMediator mediator, IConfigurationQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCommand request) => await _mediator.Send(request);

        [HttpGet]
        public async Task<IActionResult> Get(ConfigurationRequest request) => await _query.GetAsync(request);
    }
}