using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automat.Application.Handlers.Payment.PaymentTypeOption.Commands;
using MediatR;
using SharedLibrary.Controller;

namespace Automat.ManagementApi.Controllers
{
    public class PaymentTypeOptionController : BaseController
    {
        private readonly IMediator _mediator;
        public PaymentTypeOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Post([FromBody] AddPaymentTypeOptionCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}
