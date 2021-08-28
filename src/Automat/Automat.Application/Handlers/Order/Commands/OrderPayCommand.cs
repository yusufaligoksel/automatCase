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
        public string ProcessId { get; set; }
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
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderProductFeatureOptionService _orderProductFeatureOptionService;
        public OrderPayCommandHandler(IValidator<OrderPayCommand> payValidator,
            IShoppingCartService shoppingCartService,
            IProductService productService,
            IProcessService processService,
            IPaymentTypeOptionService paymentTypeOptionService,
            ICategoryFeatureOptionService categoryFeatureOptionService,
            IOrderService orderService,
            IOrderDetailService orderDetailService,
            IOrderProductFeatureOptionService orderProductFeatureOptionService)
        {
            _payValidator = payValidator;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _processService = processService;
            _paymentTypeOptionService = paymentTypeOptionService;
            _categoryFeatureOptionService = categoryFeatureOptionService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _orderProductFeatureOptionService = orderProductFeatureOptionService;
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

                Guid processId = new Guid(request.ProcessId);
                var cart = await _shoppingCartService.GetCartByProcessId(processId);

                if (cart == null)
                {
                    ErrorResult error = new("Hatalı işlem numarası! Sipariş oluşturulamaz.");
                    return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 400);
                }

                decimal paymentTotal = PriceCalculateHelper.CalculatePaymentTotal(cart.Quantity, cart.UnitPrice);
                var paymentTypeOption = await _paymentTypeOptionService.GetById(cart.PaymentTypeOptionId.Value);
                var product = await _productService.GetById(cart.ProductId);
                CategoryFeatureOption categoryFeatureOption = new CategoryFeatureOption();

                #region Order
                var order = new Domain.Entities.Order
                {
                    OrderCode = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    AutomatSlotId = cart.AutomatSlotId,
                    ProcessId = cart.ProcessId,
                    OrderStatus = (byte)OrderStatus.Complete,
                    PaymentTotal = paymentTotal,
                    PaymentTypeOptionId = paymentTypeOption.Id,
                    CreatedDate = DateTime.Now
                };
                #endregion

                #region RefundAmountControl
                if (paymentTypeOption.RefundPaymentStatus)
                {
                    if (request.PaidMoney > paymentTotal)
                    {
                        order.RefundAmount =
                            PriceCalculateHelper.CalculateRefundAmount(paymentTotal, request.PaidMoney);
                    }
                    else
                    {
                        throw new Exception("Yetersiz bakiye!");
                    }
                }
                #endregion

                await _orderService.InsertAsync(order);

                #region OrderDetail
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    CreatedDate = DateTime.Now
                };
                await _orderDetailService.InsertAsync(orderDetail);
                #endregion

                #region OrderProductFeatureOption

                if (cart.CategoryFeatureOptionId.HasValue)
                {
                    var featureOption = await _categoryFeatureOptionService.GetById(cart.CategoryFeatureOptionId.Value);
                    var orderProductFeature = new OrderProductFeatureOption
                    {
                        OrderDetailId = orderDetail.Id,
                        CategoryFeatureOptionId = featureOption.Id,
                        Quantity = cart.FeatureOptionQuantity.HasValue ? cart.FeatureOptionQuantity.Value : 0,
                        CreatedDate = DateTime.Now
                    };
                    await _orderProductFeatureOptionService.InsertAsync(orderProductFeature);
                    categoryFeatureOption = featureOption;
                }
                #endregion

                #region Result
                var result = new OrderDto
                {
                    OrderId=order.Id,
                    OrderCode = order.OrderCode,
                    OrderDate = order.OrderDate,
                    Message = "Siparişiniz oluşturuldu!",
                    RefundAmount = order.RefundAmount,
                    PaymentTotal = order.PaymentTotal,
                    Product = new OrderProductDto(product.Id,
                        product.Name,
                        product.CategoryId,
                        product.Category.Name,
                        cart.Quantity,
                        cart.CategoryFeatureOptionId,
                        categoryFeatureOption.Id > 0 ? categoryFeatureOption.Name : "-",
                        cart.FeatureOptionQuantity),
                    PaymentMethod = new OrderPaymentMethodDto(paymentTypeOption.PaymentTypeId, paymentTypeOption.PaymentType.Name, paymentTypeOption.Id, paymentTypeOption.Name)
                };
                #endregion

                //clean shopping cart
                _shoppingCartService.DeleteAsync(cart.Id);

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
