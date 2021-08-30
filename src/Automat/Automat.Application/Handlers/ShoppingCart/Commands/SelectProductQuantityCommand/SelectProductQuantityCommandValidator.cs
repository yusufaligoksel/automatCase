using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand
{
    public class SelectProductQuantityCommandValidator : AbstractValidator<SelectProductQuantityCommand>
    {
        public SelectProductQuantityCommandValidator()
        {
            RuleFor(x => x.ProcessId).NotNull().NotEmpty().WithMessage("Hatalı işlem numarası! Adet seçimi yapılamaz.");
            RuleFor(x => x.Quantity).NotEqual(0).When(x => x.Quantity < 1).WithMessage("Lütfen adet seçimi yapınız.");
        }
    }
}