using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.ProductMenuRepository
{
    public class ProductMenuRepository : IProductMenuRepository, IDisposable
    {
        private bool disposed = false;
        protected readonly ApplicationDbContext _context;

        public ProductMenuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ProductMenu> GetProductMenus(int? id = null)
        {
            IQueryable<ProductMenu> query = _context.ProductMenus
                .Include(pm => pm.Products)
                .ThenInclude(p => p.ProductImages);

            if (id != null)
                query = query.Where(pm => pm.Id == id);

            return query;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void AddMenu(ProductMenu menu)
        {
            _context.ProductMenus.Add(menu);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
