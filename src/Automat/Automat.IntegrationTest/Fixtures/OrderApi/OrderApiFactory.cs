using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Automat.IntegrationTest.Fixtures.OrderApi
{
    public class OrderApiFactory : WebApplicationFactory<Automat.OrderApi.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                config.AddConfiguration(integrationConfig);
            });

            //baslangıctaki configureServices'ten sonra cagirilir
            builder.ConfigureTestServices(services =>
            {

            });
        }
    }
}
