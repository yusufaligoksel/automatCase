using Automat.Application.Handlers.Process.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controller;
using System.Threading.Tasks;

namespace Automat.ShoppingCartApi.Controllers
{
    public class ProcessController : BaseController
    {
        private readonly IMediator _mediator;

        public ProcessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("shopping/getlastprocess")]
        public async Task<IActionResult> GetLastProcess([FromHeader] GetLastProcessQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}