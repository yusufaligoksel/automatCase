using Automat.Domain.Dtos;
using MediatR;
using SharedLibrary.Response;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automat.Persistence.Services.Abstract;
using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands
{
    public class AddToCartCommand : IRequest<GenericResponse<CartResultDto>>
    {
        public int ProductId { get; set; }
        public int? FeatureOptionId { get; set; }
        public int? FeatureOptionQuantity { get; set; }
        public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, GenericResponse<CartResultDto>>
        {
            private IValidator<AddToCartCommand> _addTocartValidator;
            private readonly IShoppingCartService _shoppingCartService;
            private readonly IProductService _productService;
            private readonly ICategoryFeatureOptionService _categoryFeatureOptionService;
            private readonly IProcessService _processService;
            public AddToCartCommandHandler(IValidator<AddToCartCommand> addTocartValidator,
                IShoppingCartService shoppingCartService,
                IProductService productService,
                ICategoryFeatureOptionService categoryFeatureOptionService,
                IProcessService processService)
            {
                _addTocartValidator = addTocartValidator;
                _shoppingCartService = shoppingCartService;
                _productService = productService;
                _categoryFeatureOptionService = categoryFeatureOptionService;
                _processService = processService;
            }
            public async Task<GenericResponse<CartResultDto>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    string feautureOptionName = "-";
                    var product = await _productService.FindAsync(request.ProductId);

                    #region Validation
                    var addCartValidResult = _addTocartValidator.Validate(request);
                    if (!addCartValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in addCartValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<CartResultDto>.ErrorResponse(error, statusCode: 400);
                    }

                    #region FeatureOption
                    if (request.FeatureOptionId.HasValue)
                    {
                        var feautureOption =
                            await _categoryFeatureOptionService.FindAsync(request.FeatureOptionId.Value);

                        if (feautureOption == null)
                        {
                            ErrorResult error = new("Ürün seçeneklerinden hatalı bir seçim yaptınız. Lütfen doğru bir seçim yapınız.");
                            return GenericResponse<CartResultDto>.ErrorResponse(error, statusCode: 400);
                        }
                        else if (feautureOption != null && feautureOption.IsSelectQuantity && (!request.FeatureOptionQuantity.HasValue || (request.FeatureOptionQuantity.HasValue && request.FeatureOptionQuantity.Value == 0)))
                        {
                            ErrorResult error = new("Ürün seçeneklerinden hatalı bir adet seçim yaptınız. Lütfen doğru bir seçim yapınız.");
                            return GenericResponse<CartResultDto>.ErrorResponse(error, statusCode: 400);
                        }
                        else
                        {
                            feautureOptionName = feautureOption.Name;
                        }
                    }
                    #endregion

                    #region Product
                    if (product != null)
                    {
                        ErrorResult error = new("Hatalı bir ürün seçimi yaptınız. Lütfen doğru bir ürün seçimi yapınız.");
                        return GenericResponse<CartResultDto>.ErrorResponse(error, statusCode: 400);
                    }
                    #endregion

                    #endregion

                    Domain.Entities.ShoppingCart cart = new Domain.Entities.ShoppingCart
                    {
                        ProcessId = _processService.GenerateProcessId(),
                        ProductId = request.ProductId,
                        PaymentTypeOptionId = request.FeatureOptionId,
                        FeatureOptionQuantity = request.FeatureOptionQuantity,
                        Quantity = Int32.MinValue,
                        UnitPrice = product.Price,
                        CreatedDate = DateTime.Now
                    };

                    await _shoppingCartService.InsertAsync(cart);

                    var result = new CartResultDto
                    {
                        ProcessId = cart.ProcessId,
                        CartItem = new CartProductDto(product.Id, product.Name, cart.FeatureOptionId, feautureOptionName, cart.FeatureOptionQuantity),
                        Message = "Ürün seçimi yapıldı."
                    };

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
