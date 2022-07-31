using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<ProductMenu>> GetProductMenu()
        {
            return await _context.ProductMenus
                .Include(pm => pm.Products)
                .ThenInclude(p => p.ProductImages)
                .ToListAsync();
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
    }
}
