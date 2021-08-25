using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automat.Application.Handlers.ShoppingCart.Commands;
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

        // GET: api/<ShoppingCartController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShoppingCartController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShoppingCartController>
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        // PUT api/<ShoppingCartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShoppingCartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
