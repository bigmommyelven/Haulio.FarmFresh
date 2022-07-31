using System.Collections.Generic;
namespace Haulio.FarmFresh.Domain.Entities
{
    public class ProductMenu : BaseEntity
    {
        public int Position { get; set; }
        public string DisplayText { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Products { get; set; }
    }
}
