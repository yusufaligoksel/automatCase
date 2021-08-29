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
            RuleFor(x => x.CategoryId).NotEqual(0).When(x => x.CategoryId < 1).When(x=>x.CategoryId==0).WithMessage("Lütfen bir kategori seçiniz.");
            RuleFor(x => x.Price).NotEqual(0).When(x => x.Price < 1).WithMessage("Lütfen ürün fiyatı giriniz.");
        }
    }
}
