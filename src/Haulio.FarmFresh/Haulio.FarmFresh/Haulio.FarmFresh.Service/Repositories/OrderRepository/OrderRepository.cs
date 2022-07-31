using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private bool disposed = false;
        protected readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Add(Order order)
        {
            var ent = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return ent.Entity;
        }

        public async Task<Order> Cancel(Order order)
        {
            var res = _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<PagedResponse> GetAll(Pagination pagination = null)
        {
            pagination ??= new Pagination();
            IQueryable<Order> query = _context.Orders
                .Include(o => o.Customers)
                .OrderByDescending(o => o.OrderDate);

            var totalRecords = query.Count();
            var data = await query
                .Skip((pagination.Page - 1) * pagination.Limit)
                .Take(pagination.Limit)
                .ToListAsync();

            return new PagedResponse(data, pagination, totalRecords);
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetByIdAndProductId(Guid id, int productId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails.Where(od => od.ProductId == productId))
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
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
