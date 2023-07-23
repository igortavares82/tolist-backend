using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Standard.ToList.Application.Commands.ProductCommands;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Requests;

namespace Standard.ToList.Api.Controllers
{
    [Route("products")]
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
        public async Task<IActionResult> Post([FromBody] CreateCommand request)
        {
            var result = await _mediator.Send(request);
            return Created($"/products/{result.Data.Id}", result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductRequest request)
        {
            var result = await _query.GetAsync(request);
            return Ok(result);
        }
    }
}

