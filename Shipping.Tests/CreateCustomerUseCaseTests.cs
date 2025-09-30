using Moq;
using Shipping.Application.DTOs;
using Shipping.Application.UseCases;
using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using System.Xml.Linq;

namespace Shipping.Tests
{
    public class CreateCustomerUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_ValidCustomer_CreatesCustomer()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            var useCase = new CreateCustomerUseCase(mockRepo.Object);

            var dto = new CreateCustomerDto
            {
                Name = "Hamide",
                Email = "J.Hamide@gmail.com",
                Address = new AddressDto
                {
                    Street = "Pasdaran St",
                    City = "Tehran",
                    Country = "Iran"
                }
            };

            var id = await useCase.ExecuteAsync(dto);
            mockRepo.Verify(r => r.SaveAsync(It.IsAny<Customer>()), Times.Once());
            Assert.NotEqual(Guid.Empty, id);
        }

        [Fact]
        public async Task ExecuteAsync_EmptyName_ThrowsException()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            var useCase = new CreateCustomerUseCase(mockRepo.Object);

            var dto = new CreateCustomerDto
            {
                Email = "J.hamide@gmail.com"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(dto));
        }
    }
}