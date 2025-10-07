using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Entities
{
    public class Order
    {
        private readonly List<OrderLine> _lines = new();

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }

        public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

        private Order() { }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public void AddLine(string sku, int qty, Money unitPrice)
        {
            if (qty <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(qty));

            _lines.Add(new OrderLine(sku, qty, unitPrice));
        }

        public Money GetTotal()
        {
            var total = _lines.Sum(x => x.Quantity * x.UnitPrice.Amount);
            return new Money(total, "USD");
        }
    }

    public class OrderLine
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public string SKU { get; }

        public int Quantity { get; }

        public Money UnitPrice { get; }

        public OrderLine() { }

        public OrderLine(string sKU, int qty, Money unitPrice)
        {
            SKU = sKU;
            Quantity = qty;
            UnitPrice = unitPrice;
        }

        public Money GetTotal() => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    }
}
