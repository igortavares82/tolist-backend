using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.LystCommands;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Common;

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
        public async Task<IActionResult> Post([FromBody] CreateCommand request)
        {
            var result = await _mediator.Send(request);
            return new CreatedResult($"/lysts/{result.Data.Id}", result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var result = await _lystQuery.GetAsync(id);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(Request request)
        {
            var result = await _lystQuery.GetAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Status == ResultStatus.NotFound)
                return NotFound();

            return NoContent();
        }
    }
}

