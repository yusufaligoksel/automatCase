using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Response;

namespace SharedLibrary.Controller
{
    [Route("api")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult CreateActionResult<T>(T response, int statusCode) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
