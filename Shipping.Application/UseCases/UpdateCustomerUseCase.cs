using Shipping.Application.DTOs;
using Shipping.Core.Interfaces;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.UseCases
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync(Guid id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException($"Customer {id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                if (dto.Name.Length < 3)
                    throw new ArgumentException("Name must be at least 3 characters", nameof(dto.Name));
                customer.UpdateName(dto.Name);
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                if (!IsvalidEmail(dto.Email))
                    throw new ArgumentException("Invalid Email Format", nameof(dto.Email));
                customer.UpdateEmail(dto.Email);
            }

            if (dto.Address != null)
            {
                customer.UpdateAddress(new Address(dto.Address.Street, dto.Address.City, dto.Address.Country));
            }

            await _customerRepository.SaveAsync(customer);
        }

        private bool IsvalidEmail(string email)
        {
            try
            {
                var m = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
