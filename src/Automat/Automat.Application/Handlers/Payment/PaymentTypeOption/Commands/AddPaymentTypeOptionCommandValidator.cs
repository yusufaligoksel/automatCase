using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.Payment.PaymentTypeOption.Commands
{
    public class AddPaymentTypeOptionCommandValidator:AbstractValidator<AddPaymentTypeOptionCommand>
    {
        public AddPaymentTypeOptionCommandValidator()
        {
            RuleFor(x => x.PaymentTypeId).NotNull().NotEmpty().WithMessage("Lütfen ödeme seçiniz.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen ödeme seçeneğinin adını giriniz.");
        }
    }
}
