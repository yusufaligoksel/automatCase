using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Application.Handlers.Order.Commands;
using Automat.Application.Handlers.Payment.PaymentType.Commands;
using Automat.Application.Handlers.Payment.PaymentTypeOption.Commands;
using Automat.Application.Handlers.Process.Queries;
using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Application.Configuration
{
    public static class BaseConfigureServices
    {
        public static IServiceCollection AddAllConfigurationServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            #region MediatR

            //shopping
            services.AddMediatR(typeof(AddToCartCommand));
            services.AddMediatR(typeof(SelectProductQuantityCommand));
            services.AddMediatR(typeof(SelectPaymentMethodCommand));
            services.AddMediatR(typeof(OrderPayCommand));
            services.AddMediatR(typeof(GetLastProcessQuery));

            //payment
            services.AddMediatR(typeof(AddPaymentTypeCommand));
            services.AddMediatR(typeof(AddPaymentTypeOptionCommand));

            #endregion

            #region FluentValidation
            services.AddTransient<IValidator<AddToCartCommand>, AddToCartCommandValidator>();
            services.AddTransient<IValidator<SelectProductQuantityCommand>, SelectProductQuantityCommandValidator>();
            services.AddTransient<IValidator<SelectPaymentMethodCommand>, SelectPaymentMethodCommandValidator>();
            services.AddTransient<IValidator<OrderPayCommand>, OrderPayCommandValidator>();
            services.AddTransient<IValidator<OrderPayCommand>, OrderPayCommandValidator>();
            services.AddTransient<IValidator<OrderPayCommand>, OrderPayCommandValidator>();
            #endregion

            return services;
        }
    }
}
