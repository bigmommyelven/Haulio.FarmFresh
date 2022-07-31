using Haulio.FarmFresh.Domain.Entities;
using System.Collections.Generic;

namespace Haulio.FarmFresh.Infrastructure.Dto
{
    public class ProductDto : CreateUpdateProductDto
    {
        public List<Tag> Tags { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductMenu> ProductMenus { get; set; }
    }
}
