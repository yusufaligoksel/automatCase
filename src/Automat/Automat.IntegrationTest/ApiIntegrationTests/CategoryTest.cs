using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Dtos;
using Automat.IntegrationTest.Fixtures.ProductApi;
using Automat.IntegrationTest.Model.Product;
using FluentAssertions;
using Newtonsoft.Json;
using SharedLibrary.Response;
using Xunit;

namespace Automat.IntegrationTest.ApiIntegrationTests
{
    public class CategoryTest : Fixtures.ProductApi.ProductApiIntegrationTestFixture
    {
        public CategoryTest(ProductApiFactory fixture) : base(fixture)
        {

        }

        [Fact]
        public async void GetCategories_Should_Be_Return_Category_List()
        {
            //Arrange
            var requestUrl = $"/api/Category";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var products = JsonConvert.DeserializeObject<GenericResponse<List<CategoryDto>>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.NotNull(products.Result);
            Assert.Null(products.ErrorResult);
        }

        [Theory]
        [InlineData(8, "Gofret")]
        public async void FindCategory_Should_Be_Return_Category_List(int id, string expectedTitle)
        {
            //Arrange
            var requestUrl = $"/api/Category/{id}";

            //Act
            var response = await _client.GetAsync(requestUrl);
            var product = JsonConvert.DeserializeObject<GenericResponse<CategoryDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(expectedTitle);
            Assert.NotNull(product.Result);
            Assert.Null(product.ErrorResult);
        }

        [Fact]
        public async void AddCategory_Should_Return_NewCategory()
        {
            //Arrange
            var entity = new NewCategoryRequest
            {
                Name = "Çerez",
                ParentId = 4
            };

            var requestUrl = "/api/Category";
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(requestUrl, content);
            var product = JsonConvert.DeserializeObject<GenericResponse<CategoryDto>>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Result.Name.Should().Be(entity.Name);
        }

        [Theory]
        [InlineData(12)]
        public async void UpdateCategory_Should_Return_UpdatedCategory(int categoryId)
        {
            //Arange
            var request = new UpdateCategoryRequest
            {
                Id = categoryId,
                Name = "Kuruyemiş",
                ParentId = 4
            };
            var putUrl = "/api/Category";
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Act
            var updatedresponse = await _client.PutAsync(putUrl, content);
            var updatedcategory = JsonConvert.DeserializeObject<GenericResponse<CategoryDto>>(
                await updatedresponse.Content.ReadAsStringAsync());

            //Assert
            updatedresponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedcategory.Result.Name.Should().Be(request.Name);
        }
    }
}
