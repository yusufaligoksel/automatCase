using Automat.Domain.Dtos;
using MediatR;
using SharedLibrary.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Automat.Application.Handlers.ShoppingCart.Commands
{
    public class AddToCartCommand : IRequest<GenericResponse<CartResultDto>>
    {
        public Guid? ProcessId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int? FeatureOptionId { get; set; }
        public int? FeatureOptionQuantity { get; set; }
        public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, GenericResponse<CartResultDto>>
        {
            public async Task<GenericResponse<CartResultDto>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = new CartResultDto();
                    result.Message = "Ürün sepete eklendi";
                    return GenericResponse<CartResultDto>.SuccessResponse(result, 200);
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<CartResultDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }

    }
}
