namespace Haulio.FarmFresh.Infrastructure.Dto
{
    public class CreateUpdateProductDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Strategy { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
