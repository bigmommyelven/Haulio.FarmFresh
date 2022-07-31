namespace Haulio.FarmFresh.Infrastructure.Dto
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
    }
}
