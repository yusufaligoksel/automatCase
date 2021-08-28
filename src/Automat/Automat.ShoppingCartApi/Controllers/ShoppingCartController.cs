using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controller;
using System.Threading.Tasks;

namespace Automat.ShoppingCartApi.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("shopping/addtocart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost]
        [Route("shopping/selectquantity")]
        public async Task<IActionResult> SelectProductQuantity([FromBody] SelectProductQuantityCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpPost]
        [Route("shopping/selectpaymentmethod")]
        public async Task<IActionResult> SelectPaymentMethod([FromBody] SelectPaymentMethodCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}