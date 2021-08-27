using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(x => x.ProductId).NotNull().When(x => x.ProductId == 0).WithMessage("Lütfen bir ürün seçiniz.");
            RuleFor(x => x.SlotId).NotNull().When(x => x.SlotId == 0).WithMessage("Slot seçiminde bir hata oluştu.");
        }
    }
}
