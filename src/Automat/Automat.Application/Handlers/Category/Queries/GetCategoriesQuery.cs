using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Category.Queries
{
    public class GetCategoriesQuery : IRequest<GenericResponse<List<CategoryDto>>>
    {
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GenericResponse<List<CategoryDto>>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            public GetCategoriesQueryHandler(ICategoryService categoryService,
                IMapper mapper)
            {
                _categoryService = categoryService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var categories = await _categoryService.GetListAsync();

                    var response = _mapper.Map<List<CategoryDto>>(categories);

                    return GenericResponse<List<CategoryDto>>.SuccessResponse(response, 200);

                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<List<CategoryDto>>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
