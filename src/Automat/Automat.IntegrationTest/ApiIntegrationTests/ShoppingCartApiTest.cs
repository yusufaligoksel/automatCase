using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Dtos;
using Automat.Domain.Entities;
using Automat.IntegrationTest.Fixtures.ShoppingCartApi;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using Xunit;

namespace Automat.IntegrationTest.ApiTests
{
    public class ShoppingCartApiTest : Fixtures.ShoppingCartApi.ShoppingCartIntegrationTestFixture
    {
        public ShoppingCartApiTest(ShoppingCartFactory fixture) : base(fixture)
        {
            
        }

        [Fact]
        public async void GetLastProcess_Should_Be_Return_Process()
        {
            //Arrange
            var requestUrl = $"/api/shopping/getlastprocess";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var process = JsonConvert.DeserializeObject<GenericResponse<ShoppingCartDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            process.Result.ProcessId.Should().NotBeEmpty();
            Assert.NotNull(process.Result.ProductId);
            Assert.Null(process.ErrorResult);
        }
    }
}
