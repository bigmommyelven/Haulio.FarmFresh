using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Customer Customers { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
