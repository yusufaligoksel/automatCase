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

namespace Automat.Application.Handlers.Payment.PaymentType.Commands
{
    public class AddPaymentTypeCommand : IRequest<GenericResponse<PaymentTypeDto>>
    {
        public string Name { get; set; }

        public class AddPaymentTypeCommandHandler : IRequestHandler<AddPaymentTypeCommand, GenericResponse<PaymentTypeDto>>
        {
            private IValidator<AddPaymentTypeCommand> _paymentValidator;
            private readonly IPaymentTypeService _paymentTypeService;

            public AddPaymentTypeCommandHandler(IValidator<AddPaymentTypeCommand> paymentValidator,
                IPaymentTypeService paymentTypeService)
            {
                _paymentValidator = paymentValidator;
                _paymentTypeService = paymentTypeService;

            }

            public async Task<GenericResponse<PaymentTypeDto>> Handle(AddPaymentTypeCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    #region Validation

                    var paymentValidResult = _paymentValidator.Validate(request);
                    if (!paymentValidResult.IsValid)
                    {
                        Dictionary<string, string> errors = new Dictionary<string, string>();
                        foreach (var item in paymentValidResult.Errors)
                            errors.Add(item.PropertyName, item.ErrorMessage);

                        ErrorResult error = new(errors);
                        return GenericResponse<PaymentTypeDto>.ErrorResponse(error, statusCode: 400);
                    }

                    #endregion

                    var paymentType = new Domain.Entities.PaymentType
                    {
                        Name = request.Name,
                        CreatedDate = DateTime.Now
                    };

                    await _paymentTypeService.InsertAsync(paymentType);

                    var result = new PaymentTypeDto(paymentType.Id, paymentType.Name);

                    return GenericResponse<PaymentTypeDto>.SuccessResponse(result, 200);
                }
                catch (Exception ex)
                {
                    ErrorResult error = new(ex.Message);
                    return GenericResponse<PaymentTypeDto>.ErrorResponse(error, statusCode: 500);
                }
            }
        }
    }

}
