using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductMenu> ProductMenus { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderId, o.ProductId });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Tags)
                .WithMany(pt => pt.Products);

            modelBuilder.Entity<ProductImage>()
                .HasKey(pi => new { pi.ProductID, pi.ImageUrl });

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductID)
                .HasConstraintName("FK_ProductImages_Product_ProductId");

            modelBuilder.Entity<ProductMenu>()
                .HasMany(pm => pm.Products)
                .WithMany(p => p.ProductMenus);

            modelBuilder.SeedApplicationData();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
