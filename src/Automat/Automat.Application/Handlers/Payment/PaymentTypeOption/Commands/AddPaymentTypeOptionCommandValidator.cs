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
            RuleFor(x => x.PaymentTypeId).NotEqual(0).When(x => x.PaymentTypeId < 1).WithMessage("Lütfen ödeme seçiniz.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen ödeme seçeneğinin adını giriniz.");
        }
    }
}
