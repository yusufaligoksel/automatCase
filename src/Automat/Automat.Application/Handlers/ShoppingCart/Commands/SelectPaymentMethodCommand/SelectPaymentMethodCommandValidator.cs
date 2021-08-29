using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand
{
    public class SelectPaymentMethodCommandValidator : AbstractValidator<SelectPaymentMethodCommand>
    {
        public SelectPaymentMethodCommandValidator()
        {
            RuleFor(x => x.ProcessId).NotNull().NotEmpty().WithMessage("Hatalı işlem numarası! Ödeme seçimi yapılamaz.");
            RuleFor(x => x.PaymentTypeOptionId).NotEqual(0).When(x => x.PaymentTypeOptionId < 1).WithMessage("Lütfen doğru ödeme tipi seçimi yapınız.");
        }
    }
}
