﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.LystCommands;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.ViewModels.Lysts;

namespace Standard.ToList.Api.Controllers
{
    [Route("lysts")]
    public class LystController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILystQuery _lystQuery;

        public LystController(IMediator mediator, ILystQuery lystQuery)
        {
            _mediator = mediator;
            _lystQuery = lystQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id) => await _lystQuery.GetAsync(id);

        [HttpGet]
        public async Task<IActionResult> Get(LystRequest request) => await _lystQuery.GetAsync(request);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteCommand request) => await _mediator.Send(request);

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCommand request) => await _mediator.Send(request);

    }
}
