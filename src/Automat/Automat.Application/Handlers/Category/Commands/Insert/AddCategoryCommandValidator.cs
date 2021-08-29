using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.Category.Commands.Insert
{
    public class AddCategoryCommandValidator:AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen bir kategori adı giriniz.");
        }
    }
}
