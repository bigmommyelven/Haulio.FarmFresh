using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Features.Order.Commands;
using Haulio.FarmFresh.Service.Features.Order.Queries;
using Haulio.FarmFresh.Service.Repositories.OrderRepository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _repo;
        private readonly IMediator _mediator;

        public OrderController(IOrderRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery { Id = id });
            var order = orders.FirstOrDefault();
            if (order == null)
                throw new NotFoundException(nameof(Order), id);

            return StatusCodeWithObject(HttpStatusCode.OK, order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Pagination pagination = null)
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery { Pagination = pagination });
            var pagedResponse = new PagedResponse(orders, pagination, 0);
            return StatusCodeWithPagination(HttpStatusCode.OK, pagedResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddOrderCommand command)
        {
            // UNDONE : CHECK BUSINESS LOGIC
            // Check Customer nya ada apa tidak
            // Check Product ada atau tidak
            // Check validasi payload
            // Perhitungan price dilakukan disini
            var result = await _mediator.Send(command);
            return StatusCodeWithObject(HttpStatusCode.OK, result);
        }

        //[Authorize]
        //[HttpDelete("{id}/product/{productId}")]
        //public async Task<IActionResult> Cancel(
        //    [FromRoute] Guid id,
        //    [FromRoute] int productId)
        //{
        //    var foundOrder = await _repo.GetByIdAndProductId(id, productId);
        //    if (foundOrder == null)
        //        throw new NotFoundException(nameof(Order), id);

        //    foreach (var od in foundOrder.OrderDetails)
        //    {
        //        od.Cancelled = true;
        //        foundOrder.Total -= od.Total;
        //    }
        //    return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Cancel(foundOrder));
        //}

        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var foundOrder = await _repo.GetById(id);
        //    if (foundOrder == null)
        //        throw new NotFoundException(nameof(Order), id);

        //    foreach (var od in foundOrder.OrderDetails)
        //    {
        //        od.Cancelled = true;
        //        foundOrder.Total -= od.Total;
        //    }
        //    await _repo.Cancel(foundOrder);
        //    return Ok();
        //}

    }
}
