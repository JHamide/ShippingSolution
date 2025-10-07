using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.ValueObjects
{

    public record Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD";

        public Money() { }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new ArgumentException("Amount can not be negative", nameof(amount));

            Amount = amount;
            Currency = currency;
        }

        public static Money Zero(string currency = "USD") => new Money(0, currency);
    }
}
