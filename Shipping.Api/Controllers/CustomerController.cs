using Microsoft.AspNetCore.Mvc;
using Shipping.Application.DTOs;
using Shipping.Application.UseCases;
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
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto, [FromServices] ICreateCustomerUseCase createCustomerUseCase)
        {
            try
            {
                var id = await createCustomerUseCase.ExecuteAsync(dto);
                return Ok(new { CustomerId = id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(Guid id, [FromServices] ICreateCustomerUseCase createCustomerUseCase)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}
