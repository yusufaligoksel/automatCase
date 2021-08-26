using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand
{
    public class SelectProductQuantityCommandValidator : AbstractValidator<SelectProductQuantityCommand>
    {
        public SelectProductQuantityCommandValidator()
        {
            RuleFor(x => x.ProcessId).NotNull().WithMessage("Hatalı işlem numarası! Adet seçimi yapılamaz.");
            RuleFor(x => x.Quantity).LessThanOrEqualTo(0).WithMessage("Lütfen adet seçimi yapınız.");
        }
    }
}
