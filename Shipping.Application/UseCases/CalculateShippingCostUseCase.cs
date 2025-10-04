using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.UseCases
{
    public class CalculateShippingCostUseCase : ICalculateShippingCostUseCase
    {
        private readonly IShippingCostCalculator _calculator;

        public CalculateShippingCostUseCase(IShippingCostCalculator calculator)
        {
            _calculator = calculator;
        }

        public Money Execute(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            return _calculator.CalculateShippingCost(order);
        }
    }
}
