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
using Automat.Application.Configuration;
using Automat.Infrastructure.Context;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;
using Automat.Persistence.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Automat.ProductApi
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Automat.ProductApi", Version = "v1" });
            });

            services.AddDbContext<AutomatContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AutomatConnection")));

            services.AddMediatR(typeof(Startup));
            services.AddAllConfigurationServices();

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Automat.ProductApi v1"));
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
