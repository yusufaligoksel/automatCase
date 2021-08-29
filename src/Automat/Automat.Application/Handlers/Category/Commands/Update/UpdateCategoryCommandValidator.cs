using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Application.Handlers.Category.Commands.Insert;
using FluentValidation;

namespace Automat.Application.Handlers.Category.Commands.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(0).When(x => x.Id < 1).WithMessage("Hatalı kategori id isteği yapıldı.");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Lütfen bir kategori adı giriniz.");
        }
    }
}
