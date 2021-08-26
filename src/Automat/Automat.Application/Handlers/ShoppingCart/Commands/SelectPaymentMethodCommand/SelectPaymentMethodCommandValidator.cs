﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand
{
    public class SelectPaymentMethodCommandValidator : AbstractValidator<SelectPaymentMethodCommand>
    {
        public SelectPaymentMethodCommandValidator()
        {
            RuleFor(x => x.ProcessId).NotNull().WithMessage("Hatalı işlem numarası! Ödeme seçimi yapılamaz.");
            RuleFor(x => x.PaymentTypeOptionId).NotNull().LessThanOrEqualTo(0).WithMessage("Lütfen doğru ödeme tipi seçimi yapınız.");
        }
    }
}