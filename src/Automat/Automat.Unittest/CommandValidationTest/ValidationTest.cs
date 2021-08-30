using Automat.Application.Handlers.Category.Commands.Insert;
using Automat.Application.Handlers.Category.Commands.Update;
using Automat.Application.Handlers.Order.Commands;
using Automat.Application.Handlers.Payment.PaymentType.Commands;
using Automat.Application.Handlers.Payment.PaymentTypeOption.Commands;
using Automat.Application.Handlers.Product.Commands.Insert;
using Automat.Application.Handlers.Product.Commands.Update;
using Automat.Application.Handlers.ShoppingCart.Commands;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectProductQuantityCommand;
using FluentAssertions;
using System;
using Xunit;

namespace Automat.Unittest.CommandValidationTest
{
    public class ValidationTest
    {
        private AddToCartCommandValidator _addToCartCommandValidator;
        private SelectProductQuantityCommandValidator _selectProductQuantityCommandValidator;
        private SelectPaymentMethodCommandValidator _selectPaymentMethodCommandValidator;
        private OrderPayCommandValidator _orderPayCommandValidator;
        private AddProductCommandValidator _addProductCommandValidator;
        private UpdateProductCommandValidator _updateProductCommandValidator;
        private AddCategoryCommandValidator _addCategoryCommandValidator;
        private UpdateCategoryCommandValidator _updateCategoryCommandValidator;
        private AddPaymentTypeCommandValidator _addPaymentTypeCommandValidator;
        private AddPaymentTypeOptionCommandValidator _addPaymentTypeOptionCommandValidator;

        public ValidationTest()
        {
            _addToCartCommandValidator = new AddToCartCommandValidator();
            _selectProductQuantityCommandValidator = new SelectProductQuantityCommandValidator();
            _selectPaymentMethodCommandValidator = new SelectPaymentMethodCommandValidator();
            _orderPayCommandValidator = new OrderPayCommandValidator();
            _addProductCommandValidator = new AddProductCommandValidator();
            _updateProductCommandValidator = new UpdateProductCommandValidator();
            _addCategoryCommandValidator = new AddCategoryCommandValidator();
            _updateCategoryCommandValidator = new UpdateCategoryCommandValidator();
            _addPaymentTypeCommandValidator = new AddPaymentTypeCommandValidator();
            _addPaymentTypeOptionCommandValidator = new AddPaymentTypeOptionCommandValidator();
        }

        [Fact]
        public void AddToCart_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new AddToCartCommand { ProductId = 0, SlotId = 0 };
            int expectedErrorCount = 2;

            //Act
            var result = _addToCartCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void SelectProductQuantity_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new SelectProductQuantityCommand() { ProcessId = String.Empty, Quantity = 0 };
            int expectedErrorCount = 2;

            //Act
            var result = _selectProductQuantityCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void SelectPaymentMethod_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new SelectPaymentMethodCommand() { ProcessId = String.Empty, PaymentTypeOptionId = 0 };
            int expectedErrorCount = 2;

            //Act
            var result = _selectPaymentMethodCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void OrderPay_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new OrderPayCommand() { ProcessId = String.Empty, PaidMoney = 0 };
            int expectedErrorCount = 2;

            //Act
            var result = _orderPayCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void InsertProduct_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new AddProductCommand() { Name = String.Empty, CategoryId = 0, Price = 0 };
            int expectedErrorCount = 3;

            //Act
            var result = _addProductCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void UpdateProduct_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new UpdateProductCommand() { Id = 0, Name = String.Empty, CategoryId = 0, Price = 0 };
            int expectedErrorCount = 4;

            //Act
            var result = _updateProductCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void InsertCategory_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new AddCategoryCommand() { Name = String.Empty };
            int expectedErrorCount = 1;

            //Act
            var result = _addCategoryCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void UpdateCategory_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new UpdateCategoryCommand() { Id = 0, Name = String.Empty };
            int expectedErrorCount = 2;

            //Act
            var result = _updateCategoryCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void AddPaymentType_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new AddPaymentTypeCommand() { Name = String.Empty };
            int expectedErrorCount = 1;

            //Act
            var result = _addPaymentTypeCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }

        [Fact]
        public void AddPaymentTypeOption_InvalidParameter_Should_Be_Return_IsValid_False()
        {
            //Arrange
            var request = new AddPaymentTypeOptionCommand() { PaymentTypeId = 0, Name = String.Empty };
            int expectedErrorCount = 2;

            //Act
            var result = _addPaymentTypeOptionCommandValidator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(expectedErrorCount);
        }
    }
}