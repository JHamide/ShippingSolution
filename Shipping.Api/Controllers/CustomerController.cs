using Microsoft.AspNetCore.Mvc;
using Shipping.Application.DTOs;
using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using Shipping.Core.ValueObjects;

namespace Shipping.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var customer = new Customer(dto.Name, dto.Email);
            if (dto.Address != null)
            {
                customer.UpdateAddress(new Address(dto.Address.street, dto.Address.city, dto.Address.country));
            }
            await _customerRepository.SaveAsync(customer);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}
