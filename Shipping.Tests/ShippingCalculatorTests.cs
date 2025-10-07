using Shipping.Core.Entities;
using Shipping.Core.ValueObjects;
using Shipping.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Tests
{
    public class ShippingCalculatorTests
    {
        [Fact]
        public void FlateRate_Returns_Constant()
        {
            //Arrange
            var calc = new FlatRateShippingCalculator();
            var order = new Order(Guid.NewGuid());
            order.AddLine("sku1", 3, new Money(10m, "USD"));

            //Act
            var cost = calc.CalculateShippingCost(order);

            //Assert
            Assert.Equal(new Money(10m, "USD"), cost);
        }

        [Fact]
        public void Percentage_Calculates_Correctly()
        {
            //Arrange
            var calc = new PercentageShippingCalculator(0.1m);
            var order = new Order(Guid.NewGuid());
            order.AddLine("sku1", 2, new Money(50m, "USD")); // total = 100

            //Act
            var cost = calc.CalculateShippingCost(order);

            //Assert
            Assert.Equal(new Money(10m, "USD"), cost); // 100 * 0.1 = 10
        }
    }
}
