using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.DTOs
{
    public class UpdateCustomerDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public AddressDto? Address { get; set; }
    }
}
