using Haulio.FarmFresh.Service.Repositories.ProductMenuRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Features.ProductMenu.Queries
{
    public class GetProductMenusQuery : IRequest<IEnumerable<Domain.Entities.ProductMenu>>
    {
        public int? Id { get; set; }

        public class GetProductMenuQueryHandler : IRequestHandler<GetProductMenusQuery, IEnumerable<Domain.Entities.ProductMenu>>
        {
            private readonly IProductMenuRepository _repo;

            public GetProductMenuQueryHandler(IProductMenuRepository repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<Domain.Entities.ProductMenu>> Handle(GetProductMenusQuery request, CancellationToken cancellationToken)
            {
                var query = _repo.GetProductMenus(request.Id);
                return await query.ToListAsync();
            }
        }
    }
}
