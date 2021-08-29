using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Automat.IntegrationTest.Fixtures.ShoppingCartApi;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Automat.IntegrationTest.Fixtures.ProductApi
{
    public abstract class ProductApiIntegrationTestFixture : IClassFixture<ProductApiFactory>
    {
        protected readonly ProductApiFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public ProductApiIntegrationTestFixture(ProductApiFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // if needed, reset the DB
            //_checkpoint.Reset(_configuration.GetConnectionString("SQL")).Wait();
        }
    }
}
