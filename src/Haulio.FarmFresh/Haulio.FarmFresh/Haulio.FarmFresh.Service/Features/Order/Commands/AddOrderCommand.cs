using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Persistence;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.OrderRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Features.Order.Commands
{
    public class AddOrderCommand : IRequest<Domain.Entities.Order>
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public List<AddOrderDetailModel> OrderDetails { get; set; }

        public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Domain.Entities.Order>
        {
            private readonly IApplicationDbContext _context;
            private readonly IOrderRepository _repo;

            public AddOrderCommandHandler(IApplicationDbContext context, IOrderRepository repo)
            {
                _context = context;
                _repo = repo;
            }

            public async Task<Domain.Entities.Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
            {
                if (_context.Customers.FirstOrDefault(c => c.Id == request.CustomerId) == null)
                    throw new NotFoundException(nameof(Customer), request.CustomerId);

                var order = new Domain.Entities.Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    EmployeeId = request.EmployeeId
                };

                _context.Orders.Attach(order);

                var orderDetails = new List<OrderDetail>();

                foreach (var od in request.OrderDetails)
                {
                    var foundProduct = _context.Products.FirstOrDefault(p => p.Id == od.ProductId);
                    if (foundProduct == null)
                        throw new NotFoundException(nameof(Product), od.ProductId);

                    var newOrderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        Quantity = od.Quantity,
                        Price = foundProduct.Price,
                        Cancelled = false,
                        Total = od.Quantity * foundProduct.Price,
                        ProductId = od.ProductId
                    };
                    order.Total += newOrderDetail.Total;
                    orderDetails.Add(newOrderDetail);
                }

                order.OrderDetails = orderDetails;
                _repo.Add(order);
                await _repo.SaveChangesAsync();
                return order;
            }
        }
    }

    public class AddOrderDetailModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
