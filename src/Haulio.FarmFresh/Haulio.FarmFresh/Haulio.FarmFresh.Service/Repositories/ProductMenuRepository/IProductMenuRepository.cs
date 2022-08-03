using Haulio.FarmFresh.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.ProductMenuRepository
{
    public interface IProductMenuRepository
    {
        IQueryable<ProductMenu> GetProductMenus(int? id = null);
        void AddMenu(ProductMenu menu);
        Task SaveChangesAsync();
    }
}