using Haulio.FarmFresh.Domain.Common;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<PagedResponse> GetAll(Pagination pagination = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
