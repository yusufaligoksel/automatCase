using Automat.Domain.Dtos;
using Automat.IntegrationTest.Fixtures.OrderApi;
using Automat.IntegrationTest.Model.Order;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class OrderApiTest : Fixtures.OrderApi.OrderApiIntegrationTestFixture
    {
        public OrderApiTest(OrderApiFactory fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(7, 6)]
        public async void FindProduct_Should_Be_Return_Product_List(int id, decimal expectedPaymentTotal)
        {
            //Arrange
            var requestUrl = $"/api/orders/{id}";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var product = JsonConvert.DeserializeObject<GenericResponse<OrderDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.PaymentTotal.Should().Be(expectedPaymentTotal);
            Assert.NotNull(product.Result);
            Assert.Null(product.ErrorResult);
        }

        [Theory]
        [InlineData("93a32142-b301-496f-b828-baff0d3761b3", 10, 4)]
        public async void OrderPay_Should_Return_Success(string processId, decimal paidMoney, decimal expectedRefundAmount)
        {
            //Arrange
            var entity = new OrderPayRequest
            {
                ProcessId = processId,
                PaidMoney = paidMoney
            };

            var requestUrl = "/api/order/pay";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var product = JsonConvert.DeserializeObject<GenericResponse<OrderDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.RefundAmount.Should().Be(expectedRefundAmount);
        }
    }
}