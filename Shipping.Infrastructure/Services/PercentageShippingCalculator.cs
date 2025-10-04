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
    public class PercentageShippingCalculator : IShippingCostCalculator
    {
        private readonly decimal _rate;

        public PercentageShippingCalculator(decimal rate)
        {
            if (rate < 0) throw new ArgumentException("rate must be >= 0", nameof(rate));
            _rate = rate;
        }

        public Money CalculateShippingCost(Order order)
        {
            var total = order.Total();
            var cost = Math.Round(total.Amount * _rate, 2);
            return new Money(cost, total.Currency);
        }
    }
}
