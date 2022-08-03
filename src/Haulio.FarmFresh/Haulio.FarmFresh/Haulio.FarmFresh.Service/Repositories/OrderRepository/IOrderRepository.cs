using Haulio.FarmFresh.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.OrderRepository
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        void Add(Order order);
        void Update(Order order);
        Task SaveChangesAsync();
    }
}
