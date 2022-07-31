using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        Task<PagedResponse> GetAll(Pagination pagination = null);
        Task<Order> GetById(Guid id);
        Task<Order> Add(Order order);
        Task<Order> Cancel(Order order);
        Task<Order> GetByIdAndProductId(Guid id, int productId);
    }
}
