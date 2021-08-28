using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Application.Handlers.Order.Commands;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Order.Queries
{
    public class GetOrderQuery : IRequest<GenericResponse<OrderDto>>
    {
        public int Id { get; set; }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GenericResponse<OrderDto>>
    {
        private readonly IOrderService _orderService;
        public GetOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GenericResponse<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderService.GetById(request.Id);
                if (order != null)
                {
                    var orderDetail = order.OrderDetails.FirstOrDefault();
                    var orderProductFeatureOption = orderDetail.OrderProductFeatureOptions.FirstOrDefault();
                    #region Result
                    var result = new OrderDto
                    {
                        OrderId = order.Id,
                        OrderCode = order.OrderCode,
                        OrderDate = order.OrderDate,
                        Message = "Siparişiniz bilgileri çekildi.",
                        RefundAmount = order.RefundAmount,
                        PaymentTotal = order.PaymentTotal,
                        Product = new OrderProductDto(orderDetail.ProductId,
                            orderDetail.Product.Name,
                            orderDetail.Product.CategoryId,
                            orderDetail.Product.Category.Name,
                            orderDetail.Quantity,
                            orderProductFeatureOption != null ? orderProductFeatureOption.CategoryFeatureOptionId : null,
                            orderProductFeatureOption != null ? orderProductFeatureOption.CategoryFeatureOption.Name : "-",
                            orderProductFeatureOption != null ? orderProductFeatureOption.Quantity : null),
                        PaymentMethod = new OrderPaymentMethodDto(order.PaymentTypeOption.PaymentTypeId, order.PaymentTypeOption.PaymentType.Name, order.PaymentTypeOptionId, order.PaymentTypeOption.Name)
                    };
                    #endregion

                    return GenericResponse<OrderDto>.SuccessResponse(result, 200);
                }
                else
                {
                    ErrorResult error = new("Sipariş bulunamadı.");
                    return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 500);
                }
            }
            catch (Exception ex)
            {
                ErrorResult error = new(ex.Message);
                return GenericResponse<OrderDto>.ErrorResponse(error, statusCode: 500);
            }
        }
    }
}
