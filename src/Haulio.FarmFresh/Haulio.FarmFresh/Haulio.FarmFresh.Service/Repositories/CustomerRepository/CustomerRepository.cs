using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.CustomerRepository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Order> GetCustomerOrderById(int custId, Guid orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.CustomerId == custId && o.Id == orderId);

        }

        public async Task<IEnumerable<Order>> GetCustomerOrders(int id)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == id)
                .ToListAsync();
        }
    }
}
