using Automat.Domain.Dtos;
using Automat.IntegrationTest.Fixtures.ShoppingCartApi;
using Automat.IntegrationTest.Model.ShoppingCart;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class ShoppingCartApiTest : Fixtures.ShoppingCartApi.ShoppingCartIntegrationTestFixture
    {
        public ShoppingCartApiTest(ShoppingCartFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public async void AddToCart_Should_Return_Success()
        {
            //Arrange
            var entity = new AddToCartRequest
            {
                SlotId = 1,
                ProductId = 1,
                FeatureOptionId = 1,
                FeatureOptionQuantity = 2
            };

            var requestUrl = "/api/shopping/addtocart";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var result = JsonConvert.DeserializeObject<GenericResponse<CartResultDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.NotNull(result.Result.ProcessId);
        }

        [Theory]
        [InlineData("93a32142-b301-496f-b828-baff0d3761b3", 1, 2)]
        public async void SelectQuantity_Should_Return_Success(string processId, int productId, int quantity)
        {
            //Arrange
            var entity = new SelectProductQuantityRequest
            {
                ProcessId = processId,
                ProductId = productId,
                Quantity = quantity
            };

            var requestUrl = "/api/shopping/selectquantity";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var result = JsonConvert.DeserializeObject<GenericResponse<SelectQuantityResultDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.Quantity.Should().Be(quantity);
            Assert.NotNull(result.Result.ProcessId);
        }

        [Theory]
        [InlineData("93a32142-b301-496f-b828-baff0d3761b3", 2, 3)]
        public async void SelectPaymentMethod_Should_Return_Success(string processId, int paymentTypeId, int paymentTypeOptionId)
        {
            //Arrange
            var entity = new SelectPaymentMethodRequest
            {
                ProcessId = processId,
                PaymentTypeOptionId = paymentTypeOptionId
            };

            var requestUrl = "/api/shopping/selectpaymentmethod";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var result = JsonConvert.DeserializeObject<GenericResponse<SelectPaymentMethodResultDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Result.PaymentTypeId.Should().Be(paymentTypeId);
            Assert.NotNull(result.Result.ProcessId);
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