using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automat.Application.Handlers.Order.Commands;
using Automat.Application.Handlers.Process.Queries;
using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using Automat.Infrastructure.Context;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;
using Automat.Persistence.Services.Concrete;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Automat.OrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Automat.OrderApi", Version = "v1" });
            });

            services.AddDbContext<AutomatContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AutomatConnection")));

            #region MediatR
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(AddToCartCommand));
            services.AddMediatR(typeof(SelectProductQuantityCommand));
            services.AddMediatR(typeof(SelectPaymentMethodCommand));
            services.AddMediatR(typeof(OrderPayCommand));
            services.AddMediatR(typeof(GetLastProcessQueries));
            #endregion

            #region FluentValidation
            services.AddTransient<IValidator<AddToCartCommand>, AddToCartCommandValidator>();
            services.AddTransient<IValidator<SelectProductQuantityCommand>, SelectProductQuantityCommandValidator>();
            services.AddTransient<IValidator<SelectPaymentMethodCommand>, SelectPaymentMethodCommandValidator>();
            services.AddTransient<IValidator<OrderPayCommand>, OrderPayCommandValidator>();
            #endregion

            #region ServiceInjection
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICategoryFeatureService, CategoryFeatureService>();
            services.AddScoped<ICategoryFeatureOptionService, CategoryFeatureOptionService>();
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IPaymentTypeOptionService, PaymentTypeOptionService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IAutomatSlotService, AutomatSlotService>();
            services.AddScoped<IAutomatSlotProductService, AutomatSlotProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderProductFeatureOptionService, OrderProductFeatureOptionService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Automat.OrderApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
