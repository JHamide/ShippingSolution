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
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> ExecuteAsync(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Name cannot be empty", nameof(dto.Name));

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email cannot be empty", nameof(dto.Email));

            if (dto.Name.Length < 3)
                throw new ArgumentException("Name must be at least 3 characters", nameof(dto.Name));

            var customer = new Customer(dto.Name, dto.Email);

            if (dto.Address != null)
            {
                var address = new Address(dto.Address.Street, dto.Address.City, dto.Address.Country);
                customer.UpdateAddress(address);
            }

            await _customerRepository.SaveAsync(customer);

            return customer.Id;
        }
    }
}
