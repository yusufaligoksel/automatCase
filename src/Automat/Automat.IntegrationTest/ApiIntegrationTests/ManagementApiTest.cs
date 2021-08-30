using Automat.Domain.Dtos;
using Automat.IntegrationTest.Fixtures.ManagementApi;
using Automat.IntegrationTest.Model.Management;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class ManagementApiTest : Fixtures.ManagementApi.ManagementApiIntegrationTestFixture
    {
        public ManagementApiTest(ManagementApiFactory fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData("Takı")]
        public async void AddPaymentType_Should_Return_NewPaymentType(string name)
        {
            //Arrange
            var entity = new AddPaymentTypeRequest
            {
                Name = name
            };

            var requestUrl = "/api/PaymentType";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var product = JsonConvert.DeserializeObject<GenericResponse<PaymentTypeDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(entity.Name);
            product.Result.Id.Should().NotBe(0);
        }

        [Theory]
        [InlineData(4, "Bilezik")]
        public async void AddPaymentTypeOption_Should_Return_NewPaymentType(int paymentTypeId, string name)
        {
            //Arrange
            var entity = new AddPaymentTypeOptionRequest
            {
                PaymentTypeId = paymentTypeId,
                Name = name
            };

            var requestUrl = "/api/PaymentTypeOption";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var product = JsonConvert.DeserializeObject<GenericResponse<PaymentTypeOptionDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(entity.Name);
            product.Result.Id.Should().NotBe(0);
        }
    }
}