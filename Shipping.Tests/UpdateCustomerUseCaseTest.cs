using Moq;
using Shipping.Application.DTOs;
using Shipping.Application.UseCases;
using Shipping.Core.Entities;
using Shipping.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shipping.Tests
{
    public class UpdateCustomerUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ValidUpdate_SavesCustomer()
        {
            //Arrange
            var moqRepo = new Mock<ICustomerRepository>();
            var existing = new Customer("OldName", "Old@example.com");
            moqRepo.Setup(r => r.GetByIdAsync(existing.Id)).ReturnsAsync(existing);

            var useCase = new UpdateCustomerUseCase(moqRepo.Object);

            var dto = new UpdateCustomerDto { Name = "NewName", Email = "New@example.com" };


            //Act
            await useCase.ExecuteAsync(existing.Id, dto);


            //Assert
            moqRepo.Verify(r => r.SaveAsync(It.Is<Customer>(c => c.Id == existing.Id && c.Name == "NewName" && c.Email == "New@example.com")), Times.Once());

        }

        [Fact]
        public async Task ExecuteAsync_NotFound_ThrowsKeyNotFound()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Customer?)null);

            var useCase = new UpdateCustomerUseCase(mockRepo.Object);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.ExecuteAsync(Guid.NewGuid(), new UpdateCustomerDto()));
        }
    }
}
