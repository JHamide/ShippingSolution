using Shipping.Application.DTOs;
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
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderRepository _repo;
        public CreateOrderUseCase(IOrderRepository repo) => _repo = repo;

        public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
        {
            var order = new Order(Guid.NewGuid());
            foreach (var l in request.Lines)
            {
                order.AddLine(l.SKU, l.Quantity, new Money(l.UnitPrice, "USD"));
            }

            await _repo.SaveAsync(order);
            return order.Id;
        }
    }
}
