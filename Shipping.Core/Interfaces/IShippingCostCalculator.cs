using Shipping.Core.Entities;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Interfaces
{
    public interface IShippingCostCalculator
    {
        Money CalculateShippingCost(Order order);
    }
}
