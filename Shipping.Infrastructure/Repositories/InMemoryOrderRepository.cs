using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly ConcurrentDictionary<Guid, Order> _store = new();

        public Task<Order?> GetByIdAsync(Guid id) =>
            Task.FromResult(_store.TryGetValue(id, out var o) ? o : null);

        public Task SaveAsync(Order order)
        {
            _store[order.Id] = order;
            return Task.CompletedTask;
        }
    }
}
