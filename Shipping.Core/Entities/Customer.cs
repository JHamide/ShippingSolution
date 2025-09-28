using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Entities
{
    public class Customer(Guid? id = null)
    {
        public Guid Id { get; private set; } = id ?? Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
