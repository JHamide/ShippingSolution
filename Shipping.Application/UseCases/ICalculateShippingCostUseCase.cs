using Shipping.Application.DTOs;
using Shipping.Core.Entities;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.UseCases
{
    public interface ICalculateShippingCostUseCase
    {
        Money Execute(Order order);
    }
}
