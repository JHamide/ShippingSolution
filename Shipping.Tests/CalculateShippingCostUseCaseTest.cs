using Moq;
using Shipping.Application.UseCases;
using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Tests
{
    public class CalculateShippingCostUseCaseTest
    {
        [Fact]
        public void Execute_Should_CallCalculator_AndReturnMoney()
        {
            //Arrange
            var mockCalc = new Mock<IShippingCostCalculator>();
            var expected = new Money(15m, "usd");
            mockCalc.Setup(c => c.CalculateShippingCost(It.IsAny<Order>())).Returns(expected);

            var useCase = new CalculateShippingCostUseCase(mockCalc.Object);

            var order = new Order();
            order.AddLine("sku1", 2, new Money(5m, "usd"));

            //Act
            var result = useCase.Execute(order);

            //Assert
            Assert.Equal(expected, result);
            mockCalc.Verify(c => c.CalculateShippingCost(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void Execute_NullOrder_ThrowsArgumentNullException()
        {
            var mockCalc = new Mock<IShippingCostCalculator>();
            var useCase = new CalculateShippingCostUseCase(mockCalc.Object);

            Assert.Throws<ArgumentNullException>(() => useCase.Execute(null!));
        }
    }
}
