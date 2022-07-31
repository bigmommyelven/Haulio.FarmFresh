using Haulio.FarmFresh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Order>> GetCustomerOrders(int id);
        Task<Order> GetCustomerOrderById(int custId, Guid orderId);
    }
}
