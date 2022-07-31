using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Strategy { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductMenu> ProductMenus { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
