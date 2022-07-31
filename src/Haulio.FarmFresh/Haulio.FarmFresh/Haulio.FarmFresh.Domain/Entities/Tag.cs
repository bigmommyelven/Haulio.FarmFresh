using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haulio.FarmFresh.Domain.Entities
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public List<Product> Products { get; set; }
    }
}
