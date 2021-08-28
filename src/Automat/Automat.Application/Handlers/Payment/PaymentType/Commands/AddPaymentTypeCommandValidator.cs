using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.Payment.PaymentType.Commands
{
    public class AddPaymentTypeCommandValidator:AbstractValidator<AddPaymentTypeCommand>
    {
        public AddPaymentTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen ödeme tipi adını giriniz.");
        }
    }
}
