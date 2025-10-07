using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.ValueObjects
{
    public record Address
    {
        public string Street { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

        public Address() { }

        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
        }

        public override string ToString() => $"{Street}, {City}, {Country}";
    }
}
