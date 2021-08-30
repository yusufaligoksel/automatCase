using Automat.Common.Helpers;
using FluentAssertions;
using System;
using Xunit;

namespace Automat.Unittest.HelperTests
{
    public class CalculateTest
    {
        [Theory]
        [InlineData(14.45, 2, 28.9)]
        public void Calculate_PaymentTotal_Should_Be_Return_PaymentTotal(decimal unitPrice, int quantity, decimal expectedValue)
        {
            //Act
            var result = PriceCalculateHelper.CalculatePaymentTotal(quantity, unitPrice);

            //Assert
            result.Should().Be(expectedValue);
            result.Should().NotBe(0);
        }

        [Theory]
        [InlineData(14.45)]
        public void Calculate_PaymentTotal_InValiParameter_Should_Be_Return_Exception(decimal unitPrice)
        {
            //Assert
            int? quantity = null;

            //Act
            Action action = () =>
            {
                PriceCalculateHelper.CalculatePaymentTotal(quantity.Value, unitPrice);
            };

            //Assert
            action.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(25.5, 32, 6.5)]
        public void Calculate_RefundAmount_Should_Be_Return_RefundAmount(decimal paymentTotal, decimal paidMoney, decimal expectedValue)
        {
            //Act
            var result = PriceCalculateHelper.CalculateRefundAmount(paymentTotal, paidMoney);

            //Assert
            result.Should().Be(expectedValue);
            result.Should().NotBe(0);
        }

        [Theory]
        [InlineData(50, 27.8)]
        public void Calculate_RefundAmount_InValiParameter_Should_Be_Return_Exception(decimal paymentTotal, decimal paidMoney)
        {
            //Act
            Action action = () =>
            {
                PriceCalculateHelper.CalculateRefundAmount(paymentTotal, paidMoney);
            };

            //Assert
            action.Should().Throw<Exception>();
        }
    }
}