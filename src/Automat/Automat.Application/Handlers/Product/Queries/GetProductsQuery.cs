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
    public class GetProductsQuery:IRequest<GenericResponse<List<ProductDto>>>
    {
        public class GetProductsQueryHandler:IRequestHandler<GetProductsQuery, GenericResponse<List<ProductDto>>>
        {
            private readonly IProductService _productService;
            private readonly IMapper _mapper;
            public GetProductsQueryHandler(IProductService productService,
                IMapper mapper)
            {
                _productService = productService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _productService.GetListAsync();

                    var response = _mapper.Map<List<ProductDto>>(products);

                    return GenericResponse<List<ProductDto>>.SuccessResponse(response, 200);

                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<List<ProductDto>>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
