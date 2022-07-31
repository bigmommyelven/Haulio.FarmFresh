using AutoMapper;
using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Infrastructure.Dto;
using Haulio.FarmFresh.Persistence;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.OrderRepository;
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
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public OrderController(IOrderRepository repo, IMapper mapper, IApplicationDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _repo.GetById(id);
            return StatusCodeWithObject(HttpStatusCode.OK, order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Pagination pagination = null)
        {
            var entList = await _repo.GetAll(pagination);
            return StatusCodeWithPagination(HttpStatusCode.OK, entList);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(OrderDto request)
        {
            // UNDONE : CHECK BUSINESS LOGIC
            // Check Customer nya ada apa tidak
            // Check Product ada atau tidak
            // Check validasi payload
            // Perhitungan price dilakukan disini

            if (_context.Customers.FirstOrDefault(c => c.Id == request.CustomerId) == null)
                throw new NotFoundException("Customer", request.CustomerId);

            request.OrderDetails.ForEach(od =>
            {
                var foundProduct = _context.Products.FirstOrDefault(p => p.Id == od.ProductId);
                if (foundProduct == null)
                    throw new NotFoundException("Product", od.ProductId);

                od.Total = foundProduct.Price * od.Quantity;
                od.Price = foundProduct.Price;
                request.Total += od.Total;
            });

            var order = _mapper.Map<Order>(request);
            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Add(order));
        }

        [Authorize]
        [HttpDelete("{id}/product/{productId}")]
        public async Task<IActionResult> Cancel(
            [FromRoute] Guid id,
            [FromRoute] int productId)
        {
            var foundOrder = await _repo.GetByIdAndProductId(id, productId);
            if (foundOrder == null)
                throw new NotFoundException(nameof(Order), id);

            foreach (var od in foundOrder.OrderDetails)
            {
                od.Cancelled = true;
                foundOrder.Total -= od.Total;
            }
            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Cancel(foundOrder));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var foundOrder = await _repo.GetById(id);
            if (foundOrder == null)
                throw new NotFoundException(nameof(Order), id);

            foreach (var od in foundOrder.OrderDetails)
            {
                od.Cancelled = true;
                foundOrder.Total -= od.Total;
            }
            await _repo.Cancel(foundOrder);
            return Ok();
        }

    }
}
