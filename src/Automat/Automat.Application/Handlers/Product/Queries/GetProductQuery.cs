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

namespace Automat.Application.Handlers.Product.Queries
{
    public class GetProductQuery : IRequest<GenericResponse<ProductDto>>
    {
        public int Id { get; set; }
        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GenericResponse<ProductDto>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;
            public GetProductQueryHandler(IProductService productService,
                IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _productService.FindAsync(request.Id);

                    var response = _mapper.Map<ProductDto>(product);

                    return GenericResponse<ProductDto>.SuccessResponse(response, 200);

                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<ProductDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
