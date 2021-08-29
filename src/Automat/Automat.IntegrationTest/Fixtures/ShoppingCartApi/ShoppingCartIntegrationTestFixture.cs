using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Automat.IntegrationTest.Fixtures.ShoppingCartApi
{
    public abstract class ShoppingCartIntegrationTestFixture : IClassFixture<ShoppingCartFactory>
    {
        protected readonly ShoppingCartFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public ShoppingCartIntegrationTestFixture(ShoppingCartFactory fixture)
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
