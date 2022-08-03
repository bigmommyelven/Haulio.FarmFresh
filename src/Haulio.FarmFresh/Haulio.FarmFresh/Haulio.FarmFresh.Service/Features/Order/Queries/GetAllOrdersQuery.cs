using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Service.Repositories.OrderRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Features.Order.Queries
{
    public class GetAllOrdersQuery : IRequest<List<Domain.Entities.Order>>
    {
        public Pagination Pagination { get; set; }
        public Guid? Id { get; set; }

        public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<Domain.Entities.Order>>
        {
            private readonly IOrderRepository _repo;
            public GetAllOrdersQueryHandler(IOrderRepository repo)
            {
                _repo = repo;
            }
            public async Task<List<Domain.Entities.Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Domain.Entities.Order> query = _repo.GetAll()
                    .Include(o => o.Customers)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new Domain.Entities.Order
                    {
                        Id = o.Id,
                        Customers = o.Customers,
                        EmployeeId = o.EmployeeId,
                        OrderDate = o.OrderDate,
                        OrderDetails = o.OrderDetails.Select(od => new OrderDetail
                        {
                            Product = od.Product,
                            Price = od.Price,
                            Cancelled = od.Cancelled,
                            Quantity = od.Quantity,
                            Total = od.Total
                        }).ToList(),
                        Total = o.Total
                    });

                if (request.Id != null)
                {
                    query = query.Where(o => o.Id == request.Id);
                }
                if (request.Pagination != null)
                {
                    query = query.Skip((request.Pagination.Page - 1) * request.Pagination.Limit)
                        .Take(request.Pagination.Limit);
                }
                Console.WriteLine(query.ToQueryString());

                return await query.ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
