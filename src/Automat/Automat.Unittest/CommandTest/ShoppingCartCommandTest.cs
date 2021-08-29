using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Automat.Application.Handlers.ShoppingCart.Commands.SelectPaymentMethodCommand;
using Automat.Domain.Entities;
using Automat.Persistence.Services.Abstract;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace Automat.Unittest.CommandTest
{
    public class ShoppingCartCommandTest
    {
        private Mock<IValidator<SelectPaymentMethodCommand>> _selectPaymentMethodValidatorMock;
        private Mock<IShoppingCartService> _shoppingCartServiceMock;
        private Mock<IPaymentTypeOptionService> _paymentTypeOptionServiceMock;
        private Mock<IProcessService> _processServiceMock;
        private SelectPaymentMethodCommandHandler _selectPaymentMethodCommandHandler;
        public ShoppingCartCommandTest()
        {
            _selectPaymentMethodValidatorMock = new Mock<IValidator<SelectPaymentMethodCommand>>(MockBehavior.Strict);
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _paymentTypeOptionServiceMock = new Mock<IPaymentTypeOptionService>();
            _processServiceMock = new Mock<IProcessService>();
            _selectPaymentMethodCommandHandler = new SelectPaymentMethodCommandHandler(
                _selectPaymentMethodValidatorMock.Object,
                _shoppingCartServiceMock.Object, _processServiceMock.Object, _paymentTypeOptionServiceMock.Object);
        }

        [Fact]
        public async void SelectPaymentMethod_Null_PaymentType_Should_Be_Return_BadRequest()
        {
            //Arrange
            PaymentTypeOption paymentTypeOption = null;
            var mockData = Task<PaymentTypeOption>.Factory.StartNew(() => paymentTypeOption);
            var command = new SelectPaymentMethodCommand();
            _paymentTypeOptionServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockData);
            _selectPaymentMethodValidatorMock
                .Setup(x => x.Validate(command))
                .Returns(new ValidationResult());

            //Act
            Action action = () =>
            {
               var result=_selectPaymentMethodCommandHandler.Handle(command, new System.Threading.CancellationToken());
               result.Result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            };

            //Assert
            action.Should().NotThrow<Exception>();
            _paymentTypeOptionServiceMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            _selectPaymentMethodValidatorMock.Verify(x => x.Validate(command), Times.Once);

        }
    }
}
