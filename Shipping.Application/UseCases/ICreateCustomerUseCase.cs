using Shipping.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.UseCases
{
    public interface ICreateCustomerUseCase
    {
        Task<Guid> ExecuteAsync(CreateCustomerDto dto);
    }
}
