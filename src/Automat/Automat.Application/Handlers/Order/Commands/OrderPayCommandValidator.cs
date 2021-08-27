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
            RuleFor(x => x.ProcessId).NotNull().WithMessage("Hatalı işlem numarası! Sipariş oluşturulamaz.");
        }
    }
}
