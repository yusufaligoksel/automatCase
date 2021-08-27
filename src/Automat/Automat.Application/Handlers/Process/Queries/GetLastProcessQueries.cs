using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Persistence.Services.Abstract;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Process.Queries
{
    public class GetLastProcessQueries:IRequest<GenericResponse<Guid>>
    {
        public class GetLastProcessQueriesHandler:IRequestHandler<GetLastProcessQueries,GenericResponse<Guid>>
        {
            private readonly IShoppingCartService _shoppingCartService;
            public GetLastProcessQueriesHandler(IShoppingCartService shoppingCartService)
            {
                _shoppingCartService = shoppingCartService;
            }
            public async Task<GenericResponse<Guid>> Handle(GetLastProcessQueries request, CancellationToken cancellationToken)
            {
                var process = await _shoppingCartService.GetLastProcess();

                return GenericResponse<Guid>.SuccessResponse(process.ProcessId,200);
            }
        }
    }
}
