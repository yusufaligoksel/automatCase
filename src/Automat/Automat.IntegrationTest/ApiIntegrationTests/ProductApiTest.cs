using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Dtos;
using Automat.Domain.Entities;
using Automat.IntegrationTest.Fixtures.ProductApi;
using Automat.IntegrationTest.Model.Product;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using Xunit;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class ProductApiTest : Fixtures.ProductApi.ProductApiIntegrationTestFixture
    {
        public ProductApiTest(ProductApiFactory fixture) : base(fixture)
        {

        }

        [Fact]
        public async void GetProducts_Should_Be_Return_Product_List()
        {
            //Arrange
            var requestUrl = $"/api/Product";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var products = JsonConvert.DeserializeObject<GenericResponse<List<ProductDto>>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.NotNull(products.Result);
            Assert.Null(products.ErrorResult);
        }

        [Theory]
        [InlineData(9, "Red Bull")]
        [InlineData(12, "Milkshake")]
        public async void FindProduct_Should_Be_Return_Product_List(int id, string expectedTitle)
        {
            //Arrange
            var requestUrl = $"/api/Product/{id}";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var product = JsonConvert.DeserializeObject<GenericResponse<ProductDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(expectedTitle);
            Assert.NotNull(product.Result);
            Assert.Null(product.ErrorResult);
        }

        [Fact]
        public async void AddProduct_Should_Return_NewProduct()
        {
            //Arrange
            var entity = new NewProductRequest
            {
                Name = "Soda",
                CategoryId = 2,
                Price = 1.5M
            };

            var requestUrl = "/api/Product";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var product = JsonConvert.DeserializeObject<GenericResponse<ProductDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(entity.Name);
        }

        [Theory]
        [InlineData(16)]
        public async void UpdateProduct_Should_Return_UpdatedCategory(int productId)
        {
            //Arange
            var request = new UpdateProductRequest
            {
                Id = productId,
                Name = "Sade Maden Suyu",
                CategoryId = 2,
                Price = 2.5M
            };
            var putUrl = "/api/Product";
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Act
            var updatedresponse = await _client.PutAsync(putUrl, content);
            var updatedcategory = JsonConvert.DeserializeObject<GenericResponse<ProductDto>>(
                await updatedresponse.Content.ReadAsStringAsync());

            //Assert
            updatedresponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedcategory.Result.Name.Should().Be(request.Name);
        }
    }
}
