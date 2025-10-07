using Microsoft.EntityFrameworkCore;
using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Persistence
{
    internal class SqlOrderRepository : IOrderRepository
    {
        private readonly ShippingDbContext _db;
        public SqlOrderRepository(ShippingDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _db.Orders
                .Include("Lines") // include owned collection
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _db.Orders
                .Include("Lines")
                .ToListAsync();
        }

        public Task SaveAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
