using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using Automat.Common.Helpers;
using Automat.Domain.Dtos;
using Automat.Domain.Entities;
using Automat.Domain.Enums;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Order.Commands
{
    public class OrderPayCommand : IRequest<GenericResponse<OrderDto>>
    {
        public Guid ProcessId { get; set; }
        public decimal PaidMoney { get; set; }
    }

    public class OrderPayCommandHandler : IRequestHandler<OrderPayCommand, GenericResponse<OrderDto>>
    {
        private IValidator<OrderPayCommand> _payValidator;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IProcessService _processService;
        private readonly IPaymentTypeOptionService _paymentTypeOptionService;
        private readonly ICategoryFeatureOptionService _categoryFeatureOptionService;
        public OrderPayCommandHandler(IValidator<OrderPayCommand> payValidator,
            IShoppingCartService shoppingCartService,
            IProductService productService,
            IProcessService processService,
            IPaymentTypeOptionService paymentTypeOptionService,
            ICategoryFeatureOptionService categoryFeatureOptionService)
        {
            _payValidator = payValidator;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _processService = processService;
            _paymentTypeOptionService = paymentTypeOptionService;
            _categoryFeatureOptionService = categoryFeatureOptionService;
        }
        public async Task<GenericResponse<OrderDto>> Handle(OrderPayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region GeneralValidation
                var payvalidResult = _payValidator.Validate(request);
                if (!payvalidResult.IsValid)
                {
                    Dictionary<string, string> errors = new Dictionary<string, string>();
                    foreach (var item in payvalidResult.Errors)
                        errors.Add(item.PropertyName, item.ErrorMessage);

                    ErrorResult error = new(errors);
                    return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 400);
                }
                #endregion

                var cart = await _shoppingCartService.GetCartByProcessId(request.ProcessId);

                if (cart == null)
                {
                    ErrorResult error = new("Hatalı işlem numarası! Adet seçimi yapılamaz.");
                    return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 400);
                }


                decimal paymentTotal = PriceCalculatorHelper.CalculatePaymentTotal(cart.Quantity, cart.UnitPrice);
                var paymentTypeOption = await _paymentTypeOptionService.GetById(cart.FeatureOptionId.Value);
                var product = await _productService.GetById(cart.ProductId);
                CategoryFeatureOption categoryFeatureOption;

                #region Order
                var order = new Domain.Entities.Order
                {
                    OrderCode = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    SlotId = cart.SlotId,
                    ProcessId = cart.ProcessId,
                    OrderStatus = (byte)OrderStatus.Complete,
                    PaymentTotal = paymentTotal,
                    PaymentTypeOptionId = paymentTypeOption.Id
                };
                #endregion

                #region RefundAmountControl
                if (paymentTypeOption.RefundPaymentStatus)
                {
                    if (request.PaidMoney > paymentTotal)
                    {
                        order.RefundAmount =
                            PriceCalculatorHelper.CalculateRefundAmount(paymentTotal, request.PaidMoney);
                    }
                    else
                    {
                        throw new Exception("Yetersiz bakiye!");
                    }
                }
                #endregion

                #region OrderDetail
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice
                };
                #endregion

                #region OrderProductFeatureOption

                if (cart.FeatureOptionId.HasValue)
                {
                    var featureOption = await _categoryFeatureOptionService.GetById(cart.FeatureOptionId.Value);
                    var orderProductFeature = new OrderProductFeatureOption
                    {
                        OrderDetailId = orderDetail.Id,
                        FeatureOptionId = featureOption.Id,
                        Quantity = cart.FeatureOptionQuantity.HasValue ? cart.FeatureOptionQuantity.Value : 0
                    };
                    categoryFeatureOption = featureOption;
                }
                #endregion

                var result = new OrderDto();

                return GenericResponse<OrderDto>.SuccessResponse(result, 200);

            }
            catch (Exception ex)
            {
                ErrorResult error = new(ex.Message);
                return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 500);
            }
        }
    }
}
