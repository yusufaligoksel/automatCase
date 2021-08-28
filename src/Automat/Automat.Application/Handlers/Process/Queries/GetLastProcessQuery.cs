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

namespace Automat.Application.Handlers.Process.Queries
{
    public class GetLastProcessQuery:IRequest<GenericResponse<ShoppingCartDto>>
    {
        public class GetLastProcessQueriesHandler:IRequestHandler<GetLastProcessQuery,GenericResponse<ShoppingCartDto>>
        {
            private readonly IShoppingCartService _shoppingCartService;
            private readonly IMapper _mapper;
            public GetLastProcessQueriesHandler(IShoppingCartService shoppingCartService,
                IMapper mapper)
            {
                _shoppingCartService = shoppingCartService;
                _mapper = mapper;
            }
            public async Task<GenericResponse<ShoppingCartDto>> Handle(GetLastProcessQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var process = await _shoppingCartService.GetLastProcess();

                    var result = _mapper.Map<ShoppingCartDto>(process);

                    return GenericResponse<ShoppingCartDto>.SuccessResponse(result, 200);
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<ShoppingCartDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }
}
