using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class ProductImage
    {
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
