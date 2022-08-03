using Haulio.FarmFresh.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }

        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductMenu> ProductMenus { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        Task<int> SaveChangesAsync();
    }
}
