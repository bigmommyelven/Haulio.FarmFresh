
using System;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class OrderDetail
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
