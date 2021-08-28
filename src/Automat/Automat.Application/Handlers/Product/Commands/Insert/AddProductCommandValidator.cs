using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Application.Handlers.Product.Commands.Insert;
using FluentValidation;

namespace Automat.Application.Handlers.Product.Commands.Insert
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen bir ürün adı giriniz.");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().When(x=>x.CategoryId==0).WithMessage("Lütfen bir kategori seçiniz.");
            RuleFor(x => x.Price).NotNull().NotEmpty().When(x => x.Price == 0).WithMessage("Lütfen doğru bir ürün fiyatı giriniz.");
        }
    }
}
