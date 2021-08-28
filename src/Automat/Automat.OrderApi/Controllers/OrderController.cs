using Automat.Application.Handlers.Order.Commands;
using Automat.Application.Handlers.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controller;
using System.Threading.Tasks;

namespace Automat.OrderApi.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("orders/{id}")]
        public async Task<IActionResult> Get([FromHeader] GetOrderQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(request, result.StatusCode);
        }

        [HttpPost]
        [Route("order/pay")]
        public async Task<IActionResult> Pay([FromBody] OrderPayCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}