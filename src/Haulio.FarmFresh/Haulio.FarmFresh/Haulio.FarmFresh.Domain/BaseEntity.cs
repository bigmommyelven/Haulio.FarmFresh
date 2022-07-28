using System.ComponentModel.DataAnnotations;

namespace Haulio.FarmFresh.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
