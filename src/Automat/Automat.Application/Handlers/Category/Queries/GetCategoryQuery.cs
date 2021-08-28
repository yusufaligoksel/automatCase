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
    public class GetCategoryQuery : IRequest<GenericResponse<CategoryDto>>
    {
        public int Id { get; set; }
        public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GenericResponse<CategoryDto>>
        {
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            public GetCategoryQueryHandler(ICategoryService categoryService,
                IMapper mapper)
            {
                _categoryService = categoryService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _categoryService.FindAsync(request.Id);

                    var response = _mapper.Map<CategoryDto>(category);

                    return GenericResponse<CategoryDto>.SuccessResponse(response, 200);

                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<CategoryDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
