using Automat.Application.Handlers.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controller;
using System.Threading.Tasks;
using Automat.Application.Handlers.Product.Commands.Insert;
using Automat.Application.Handlers.Product.Commands.Update;

namespace Automat.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetList()
        {
            var query = new GetProductsQuery();
            var result = await _mediator.Send(query);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Get([FromHeader] GetProductQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Post([FromBody] AddProductCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPut]
        [Route("[controller]")]
        public async Task<IActionResult> Post([FromBody] UpdateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}