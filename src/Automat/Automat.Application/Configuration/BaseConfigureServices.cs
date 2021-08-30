using Automat.Application.Handlers.Category.Commands.Insert;
using Automat.Application.Handlers.Category.Commands.Update;
using Automat.Application.Handlers.Category.Queries;
using Automat.Application.Handlers.Order.Commands;
using Automat.Application.Handlers.Order.Queries;
using Automat.Application.Handlers.Payment.PaymentType.Commands;
using Automat.Application.Handlers.Payment.PaymentTypeOption.Commands;
using Automat.Application.Handlers.Process.Queries;
using Automat.Application.Handlers.Product.Commands.Insert;
using Automat.Application.Handlers.Product.Commands.Update;
using Automat.Application.Handlers.Product.Queries;
using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            services.AddMediatR(typeof(GetLastProcessQuery));

            //order
            services.AddMediatR(typeof(OrderPayCommand));
            services.AddMediatR(typeof(GetOrderQuery));

            //payment
            services.AddMediatR(typeof(AddPaymentTypeCommand));
            services.AddMediatR(typeof(AddPaymentTypeOptionCommand));

            //product
            services.AddMediatR(typeof(GetProductQuery));
            services.AddMediatR(typeof(GetProductsQuery));
            services.AddMediatR(typeof(AddProductCommand));
            services.AddMediatR(typeof(UpdateProductCommand));

            //category
            services.AddMediatR(typeof(GetCategoryQuery));
            services.AddMediatR(typeof(GetCategoriesQuery));
            services.AddMediatR(typeof(AddCategoryCommand));
            services.AddMediatR(typeof(UpdateCategoryCommand));

            #endregion MediatR

            #region FluentValidation

            services.AddTransient<IValidator<AddToCartCommand>, AddToCartCommandValidator>();
            services.AddTransient<IValidator<SelectProductQuantityCommand>, SelectProductQuantityCommandValidator>();
            services.AddTransient<IValidator<SelectPaymentMethodCommand>, SelectPaymentMethodCommandValidator>();
            services.AddTransient<IValidator<AddPaymentTypeOptionCommand>, AddPaymentTypeOptionCommandValidator>();
            services.AddTransient<IValidator<AddPaymentTypeCommand>, AddPaymentTypeCommandValidator>();
            services.AddTransient<IValidator<OrderPayCommand>, OrderPayCommandValidator>();
            services.AddTransient<IValidator<AddProductCommand>, AddProductCommandValidator>();
            services.AddTransient<IValidator<AddCategoryCommand>, AddCategoryCommandValidator>();
            services.AddTransient<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddTransient<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();

            #endregion FluentValidation

            return services;
        }
    }
}