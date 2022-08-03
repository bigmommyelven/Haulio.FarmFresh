using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
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

        public void Add(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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
