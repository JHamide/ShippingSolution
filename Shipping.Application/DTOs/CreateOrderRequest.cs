using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Application.DTOs
{
    public record CreateOrderLineDto(string SKU, int Quantity, decimal UnitPrice);
    public record CreateOrderRequest(List<CreateOrderLineDto> Lines);
}
