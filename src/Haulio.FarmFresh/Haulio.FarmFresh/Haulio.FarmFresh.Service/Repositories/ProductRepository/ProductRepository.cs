using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<PagedResponse> GetAll(Pagination pagination = null)
        {
            // UNDONE : Product Tags belum kebawa
            IQueryable<Product> query = _context.Products
                .Include(p => p.Tags)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.Id);

            var totalRecords = query.Count();
            var data = await query
                .Skip((pagination.Page - 1) * pagination.Limit)
                .Take(pagination.Limit)
                .ToListAsync();

            var resultObject = data.Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.CategoryId,
                p.Strategy,
                p.Price,
                ImageUrls = p.ProductImages.Select(pi => pi.ImageUrl).ToArray(),
                Tags = p.Tags.Select(t => t.Id).ToArray()

            });
            return new PagedResponse(resultObject, pagination, totalRecords);
        }

        public override async Task<Product> GetById(object id)
        {
            return await _context.Products
                .Include(p => p.Tags)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == System.Convert.ToInt32(id));
        }
    }
}
