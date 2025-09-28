using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
