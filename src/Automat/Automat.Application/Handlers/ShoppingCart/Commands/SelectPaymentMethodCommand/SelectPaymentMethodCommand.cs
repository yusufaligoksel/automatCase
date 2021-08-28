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

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand
{
    public class SelectPaymentMethodCommand : IRequest<GenericResponse<SelectPaymentMethodResultDto>>
    {
        public string ProcessId { get; set; }
        public int PaymentTypeOptionId { get; set; }
    }

    public class SelectPaymentMethodCommandHandler : IRequestHandler<SelectPaymentMethodCommand, GenericResponse<SelectPaymentMethodResultDto>>
    {
        private IValidator<SelectPaymentMethodCommand> _selectPaymentMethodValidator;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProcessService _processService;
        private readonly IPaymentTypeOptionService _paymentTypeOptionService;
        public SelectPaymentMethodCommandHandler(IValidator<SelectPaymentMethodCommand> selectPaymentMethodValidator,
            IShoppingCartService shoppingCartService,
            IProcessService processService,
            IPaymentTypeOptionService paymentTypeOptionService)
        {
            _selectPaymentMethodValidator = selectPaymentMethodValidator;
            _shoppingCartService = shoppingCartService;
            _processService = processService;
            _paymentTypeOptionService = paymentTypeOptionService;
        }

        public async Task<GenericResponse<SelectPaymentMethodResultDto>> Handle(SelectPaymentMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentTypeOption = await _paymentTypeOptionService.GetById(request.PaymentTypeOptionId);

                #region Validation

                #region GeneralValidation
                var selectPaymentMethodValidResult = _selectPaymentMethodValidator.Validate(request);
                if (!selectPaymentMethodValidResult.IsValid)
                {
                    Dictionary<string, string> errors = new Dictionary<string, string>();
                    foreach (var item in selectPaymentMethodValidResult.Errors)
                        errors.Add(item.PropertyName, item.ErrorMessage);

                    ErrorResult error = new(errors);
                    return GenericResponse<SelectPaymentMethodResultDto>.ErrorResponse(error, statusCode: 400);
                }
                #endregion

                #region PaymentTypeOption
                if (paymentTypeOption == null)
                {
                    ErrorResult error = new("Hatalı bir ödeme tipi seçimi yaptınız. Lütfen doğru seçimi yapınız.");
                    return GenericResponse<SelectPaymentMethodResultDto>.ErrorResponse(error, statusCode: 400);
                }
                #endregion

                #endregion

                Guid processId = new Guid(request.ProcessId);
                var cart = await _shoppingCartService.GetCartByProcessId(processId);

                if (cart == null)
                {
                    ErrorResult error = new("Hatalı işlem numarası! Ödeme tipi seçimi yapılamaz.");
                    return GenericResponse<SelectPaymentMethodResultDto>.ErrorResponse(error, statusCode: 400);
                }

                cart.PaymentTypeOptionId = request.PaymentTypeOptionId;
                cart.ModifiedDate=DateTime.Now;
                await _shoppingCartService.UpdateAsync(cart);

                var result = new SelectPaymentMethodResultDto
                {
                    PaymentTypeId = paymentTypeOption.PaymentTypeId,
                    PaymentTypeName = paymentTypeOption.PaymentType.Name,
                    PaymentTypeOptionId = paymentTypeOption.Id,
                    PaymentTypeOptionName = paymentTypeOption.Name,
                    ProcessId = cart.ProcessId,
                    Message = $"Ödeme tipi seçimi yapıldı. {paymentTypeOption.Name} olarak ödeme yapılacak."
                };

                return GenericResponse<SelectPaymentMethodResultDto>.SuccessResponse(result, 200);

            }
            catch (Exception ex)
            {
                ErrorResult error = new(ex.Message);
                return GenericResponse<SelectPaymentMethodResultDto>.ErrorResponse(error, statusCode: 500);
            }
        }
    }
}
