using AutoMapper;
using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Infrastructure.Dto;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.CustomerRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    //[Authorize]
    [ApiVersion("1.0")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _repo = customerRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // UNDONE : ERROR "Message": "Unable to cast object of type 'Haulio.FarmFresh.Infrastructure.Dto.CustomerDto' to type 'Haulio.FarmFresh.Infrastructure.Dto.ViewCustomerDto'.",
            var ent = await _repo.GetById(id);
            var customer = _mapper.Map<ViewCustomerDto>(ent);
            return StatusCodeWithObject(HttpStatusCode.OK, customer);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Pagination pagination = null)
        {
            var pagedResponse = await _repo.GetAll(pagination);
            return StatusCodeWithPagination(HttpStatusCode.OK, pagedResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto request)
        {
            var customer = _mapper.Map<Customer>(request);
            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Add(customer));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto request)
        {
            var foundCustomer = await _repo.GetById(id);

            if (foundCustomer == null)
                throw new NotFoundException(nameof(Customer), id);

            foundCustomer = _mapper.Map(request, foundCustomer);
            foundCustomer.Id = id;

            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Update(foundCustomer));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundCustomer = await _repo.GetById(id);

            if (foundCustomer == null)
                throw new NotFoundException(nameof(Customer), id);

            await _repo.Delete(foundCustomer);

            return Ok();
        }

        [HttpGet("{id}/order")]
        public async Task<IActionResult> GetCustomerOrders(int id)
        {
            var res = await _repo.GetCustomerOrders(id);
            return StatusCodeWithObject(HttpStatusCode.OK, res);
        }

        [HttpGet("{id}/order/{orderId}")]
        public async Task<IActionResult> GetCustomerOrder(int id, Guid orderId)
        {
            // UNDONE : Tampilin CustomerName aja sama orders: [{},{}]
            var res = await _repo.GetCustomerOrderById(id, orderId);
            return StatusCodeWithObject(HttpStatusCode.OK, res);
        }
    }
}
