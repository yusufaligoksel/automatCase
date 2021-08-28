using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automat.Application.Handlers.Payment.PaymentType.Commands;
using MediatR;
using SharedLibrary.Controller;

namespace Automat.ManagementApi.Controllers
{
    public class PaymentTypeController : BaseController
    {
        private readonly IMediator _mediator;
        public PaymentTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Post([FromBody] AddPaymentTypeCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}
