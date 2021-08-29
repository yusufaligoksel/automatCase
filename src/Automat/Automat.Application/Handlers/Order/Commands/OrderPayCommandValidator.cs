using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.Order.Commands
{
    public class OrderPayCommandValidator : AbstractValidator<OrderPayCommand>
    {
        public OrderPayCommandValidator()
        {
            RuleFor(x => x.ProcessId).NotNull().NotEmpty().WithMessage("Hatalı işlem numarası! Sipariş oluşturulamaz.");
            RuleFor(x => x.PaidMoney).NotEqual(0).When(x => x.PaidMoney < 1).WithMessage("Ödeme yapılan ücret bilgisi hatalı!");
        }
    }
}
