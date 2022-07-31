using System;
using System.Collections.Generic;

namespace Haulio.FarmFresh.Infrastructure.Dto
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
