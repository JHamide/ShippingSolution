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
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly ConcurrentDictionary<Guid, Customer> _store = new();

        public Task<Customer?> GetByIdAsync(Guid id) => Task.FromResult(_store.TryGetValue(id, out var customer) ? customer : null);

        public Task SaveAsync(Customer customer)
        {
            _store[customer.Id] = customer;
            return Task.CompletedTask;
        }
    }
}
