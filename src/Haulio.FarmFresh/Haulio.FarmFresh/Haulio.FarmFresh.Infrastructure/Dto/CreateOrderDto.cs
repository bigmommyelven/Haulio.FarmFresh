using Haulio.FarmFresh.Domain.Entities;
using System.Collections.Generic;

namespace Haulio.FarmFresh.Infrastructure.Dto
{
    public class CreateOrderDto : OrderDto
    {
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
