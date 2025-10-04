using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Services
{
    public class FlatRateShippingCalculator : IShippingCostCalculator
    {
        private readonly Money _flatRate;

        public FlatRateShippingCalculator()
        {
            _flatRate = new Money(10m, "USD");
        }

        public Money CalculateShippingCost(Order order) => _flatRate;
    }
}
