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
            switch (statusCode)
            {
                case 200:
                    return Success(response);
                    break;
                case 400:
                    return this.BadRequest(response);
                    break;
                case 404:
                    return this.NotFound(response);
                    break;
                case 500:
                    return this.Error(response);
                    break;
                default:
                    return this.Error(response);
                    break;
            }
        }

        [NonAction]
        private IActionResult Success<T>(T data)
        {
            return Ok(data);
        }

        [NonAction]
        private IActionResult Error<T>(T data)
        {
            return StatusCode(500, data);
        }

        [NonAction]
        private IActionResult BadRequest<T>(T data)
        {
            return BadRequest(data);
        }

        [NonAction]
        private IActionResult NotFound<T>(T data)
        {
            return NotFound(data);
        }
    }
}
