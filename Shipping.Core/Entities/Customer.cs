using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get; private set; }

        public string Email { get; private set; }

        private Customer() { }

        public Customer(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }
    }
}
