using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Automat.IntegrationTest.Fixtures.OrderApi;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Automat.IntegrationTest.Fixtures.ManagementApi
{
    public abstract class ManagementApiIntegrationTestFixture : IClassFixture<ManagementApiFactory>
    {
        protected readonly ManagementApiFactory _factory;
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public ManagementApiIntegrationTestFixture(ManagementApiFactory fixture)
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
