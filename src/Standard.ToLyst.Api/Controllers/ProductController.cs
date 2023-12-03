using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Standard.ToLyst.Application.Commands.ProductCommands;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Requests;

namespace Standard.ToLyst.Api.Controllers
{
    [Route("products")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductQuery _query;

        public ProductController(IMediator mediator, IProductQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommand request) => await _mediator.Send(request);

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductRequest request) => await _query.GetAsync(request);
    }
}

