using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand
{
    public class SelectProductQuantityCommand : IRequest<GenericResponse<SelectQuantityResultDto>>
    {
        public string ProcessId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class SelectProductQuantityCommandHandler : IRequestHandler<SelectProductQuantityCommand, GenericResponse<SelectQuantityResultDto>>
    {
        private IValidator<SelectProductQuantityCommand> _selectQuantityValidator;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IProcessService _processService;
        public SelectProductQuantityCommandHandler(IValidator<SelectProductQuantityCommand> selectQuantityValidator,
            IShoppingCartService shoppingCartService,
            IProductService productService,
            IProcessService processService)
        {
            _selectQuantityValidator = selectQuantityValidator;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _processService = processService;
        }
        public async Task<GenericResponse<SelectQuantityResultDto>> Handle(SelectProductQuantityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.FindAsync(request.ProductId);

                #region Validation

                #region GeneralValidation
                var selectQuantityValidResult = _selectQuantityValidator.Validate(request);
                if (!selectQuantityValidResult.IsValid)
                {
                    Dictionary<string, string> errors = new Dictionary<string, string>();
                    foreach (var item in selectQuantityValidResult.Errors)
                        errors.Add(item.PropertyName, item.ErrorMessage);

                    ErrorResult error = new(errors);
                    return GenericResponse<SelectQuantityResultDto>.ErrorResponse(error, statusCode: 400);
                }
                #endregion

                #region Product
                if (product == null)
                {
                    ErrorResult error = new("Hatalı bir ürün seçimi yaptınız. Lütfen doğru bir ürün seçimi yapınız.");
                    return GenericResponse<SelectQuantityResultDto>.ErrorResponse(error, statusCode: 400);
                }
                #endregion

                #endregion

                Guid processId = new Guid(request.ProcessId);
                var cart = await _shoppingCartService.GetCartByProcessId(processId);

                if (cart == null)
                {
                    ErrorResult error = new("Hatalı işlem numarası! Adet seçimi yapılamaz.");
                    return GenericResponse<SelectQuantityResultDto>.ErrorResponse(error, statusCode: 400);
                }

                cart.Quantity = request.Quantity;
                cart.ModifiedDate = DateTime.Now;
                await _shoppingCartService.UpdateAsync(cart);

                var result = new SelectQuantityResultDto
                {
                    ProductId =cart.ProductId,
                    ProductName = product.Name,
                    ProcessId = cart.ProcessId,
                    Quantity = cart.Quantity,
                    Message = $"Adet seçimi yapıldı. {product.Name} ürününden {cart.Quantity} adet seçildi."
                };

                return GenericResponse<SelectQuantityResultDto>.SuccessResponse(result, 200);

            }
            catch (Exception ex)
            {
                ErrorResult error = new(ex.Message);
                return GenericResponse<SelectQuantityResultDto>.ErrorResponse(error, statusCode: 500);
            }
        }
    }
}
