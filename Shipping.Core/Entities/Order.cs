using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }

        public List<OrderLine> Lines { get; private set; } = new();

        public Order(Guid? id = null) => Id = id ?? Guid.NewGuid();

        public void AddLine(string sku, int qty, Money unitPrice)
        {
            if (qty <= 0)
                throw new ArgumentException("Quantity must be > 0 ", nameof(qty));
            Lines.Add(new OrderLine(sku, qty, unitPrice));
        }

        public void RemoveLine(string sku)
        {
            Lines.RemoveAll(lines => lines.SKU == sku);
        }

        public Money Total()
        {
            decimal sum = 0m;
            foreach (var line in Lines)
                sum += line.Quantity * line.UnitPrice.Amount;
            return new Money(sum, "USD");
        }
    }

    public class OrderLine
    {
        public string SKU { get; }

        public int Quantity { get; }

        public Money UnitPrice { get; }

        public OrderLine(string sKU, int qty, Money unitPrice)
        {
            SKU = sKU;
            Quantity = qty;
            UnitPrice = unitPrice;
        }
    }
}
