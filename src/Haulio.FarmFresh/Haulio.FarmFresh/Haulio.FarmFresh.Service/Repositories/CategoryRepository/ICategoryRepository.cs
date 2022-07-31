using Haulio.FarmFresh.Domain.Entities;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryProducts(int id);
        Task<Category> GetCategoryProductById(int categoryId, int productId);
    }
}
