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
            RuleFor(x => x.ProductId).NotEqual(0).When(x => x.ProductId < 1).WithMessage("Lütfen bir ürün seçiniz.");
            RuleFor(x => x.SlotId).NotEqual(0).When(x => x.SlotId < 1).WithMessage("Slot seçiminde bir hata oluştu.");
        }
    }
}
