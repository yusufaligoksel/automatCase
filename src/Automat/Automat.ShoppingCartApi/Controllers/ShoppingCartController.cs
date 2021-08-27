using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using Automat.Domain.Dtos;
using MediatR;
using SharedLibrary.Controller;
using SharedLibrary.Response;

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
