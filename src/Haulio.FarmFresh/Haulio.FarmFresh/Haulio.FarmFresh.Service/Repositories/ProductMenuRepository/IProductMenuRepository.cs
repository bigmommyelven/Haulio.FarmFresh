using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Repositories.ProductMenuRepository
{
    public interface IProductMenuRepository
    {
        Task<IEnumerable<ProductMenu>> GetProductMenu();
    }
}