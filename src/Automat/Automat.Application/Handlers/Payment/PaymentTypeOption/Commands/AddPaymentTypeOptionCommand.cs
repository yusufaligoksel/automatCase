using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Application.Handlers.Payment.PaymentType.Commands;
using Automat.Domain.Dtos;
using Automat.Persistence.Services.Abstract;
using FluentValidation;
using MediatR;
using SharedLibrary.Response;

namespace Automat.Application.Handlers.Payment.PaymentTypeOption.Commands
{
    public class AddPaymentTypeOptionCommand : IRequest<GenericResponse<PaymentTypeOptionDto>>
    {
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
        public bool RefundPaymentStatus { get; set; }

        public class AddPaymentTypeOptionCommandHandler : IRequestHandler<AddPaymentTypeOptionCommand,
                GenericResponse<PaymentTypeOptionDto>>
        {
            private IValidator<AddPaymentTypeOptionCommand> _paymentOptionValidator;
            private readonly IPaymentTypeService _paymentTypeService;
            private readonly IPaymentTypeOptionService _paymentTypeOptionService;

            public AddPaymentTypeOptionCommandHandler(IValidator<AddPaymentTypeOptionCommand> paymentOptionValidator,
                IPaymentTypeService paymentTypeService,
                IPaymentTypeOptionService paymentTypeOptionService)
            {
                _paymentOptionValidator = paymentOptionValidator;
                _paymentTypeService = paymentTypeService;
                _paymentTypeOptionService = paymentTypeOptionService;

            }

            public async Task<GenericResponse<PaymentTypeOptionDto>> Handle(AddPaymentTypeOptionCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    #region Validation

                    var paymentOptionValidResult = _paymentOptionValidator.Validate(request);
                    if (!paymentOptionValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in paymentOptionValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<PaymentTypeOptionDto>.ErrorResponse(error, statusCode: 400);
                    }

                    #endregion

                    var paymentTypeOption = new Domain.Entities.PaymentTypeOption
                    {
                        PaymentTypeId = request.PaymentTypeId,
                        Name = request.Name,
                        RefundPaymentStatus = request.RefundPaymentStatus,
                        CreatedDate = DateTime.Now
                    };

                    await _paymentTypeOptionService.InsertAsync(paymentTypeOption);

                    var paymentType = await _paymentTypeService.FindAsync(request.PaymentTypeId);

                    var result = new PaymentTypeOptionDto(paymentType.Id, paymentType.Id, paymentType.Name,
                        paymentTypeOption.Name, paymentTypeOption.RefundPaymentStatus);

                    return GenericResponse<PaymentTypeOptionDto>.SuccessResponse(result, 200);
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<PaymentTypeOptionDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }

}
