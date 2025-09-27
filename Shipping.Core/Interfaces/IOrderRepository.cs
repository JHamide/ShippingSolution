using Shipping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);

        Task<Order?> GetByIdAsync(Guid id);
    }
}
