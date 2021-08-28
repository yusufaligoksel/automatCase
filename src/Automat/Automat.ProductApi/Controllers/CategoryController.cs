﻿using Automat.Application.Handlers.Category.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controller;
using System.Threading.Tasks;

namespace Automat.ProductApi.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetList([FromHeader] GetCategoriesQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Get([FromHeader] GetCategoryQuery request)
        {
            var result = await _mediator.Send(request);
            return CreateActionResult(result, result.StatusCode);
        }
    }
}