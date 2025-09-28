using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.ValueObjects
{
    public record Address(string street, string city, string country)
    {
        public override string ToString() => $"{street}, {city}, {country}";
    }
}
