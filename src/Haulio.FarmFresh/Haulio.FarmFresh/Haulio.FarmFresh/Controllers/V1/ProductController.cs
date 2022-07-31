using AutoMapper;
using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Infrastructure.Dto;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.ProductRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    //[Authorize]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // UNDONE : ProductTags (Tags) belum kebawa, kalo emang gk ada tampilkan []
            var ent = await _repo.GetById(id);
            var product = _mapper.Map<ProductDto>(ent);
            var resultObject = new
            {
                product.Name,
                product.Description,
                product.Price,
                product.Strategy,
                product.CategoryId,
                ImageUrls = product.ProductImages.Select(pi => pi.ImageUrl).ToArray(),
                Tags = product.Tags.Select(t => t.Id)
            };
            return StatusCodeWithObject(HttpStatusCode.OK, resultObject);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] Pagination pagination = null)
        {
            var pagedResponse = await _repo.GetAll(pagination);
            
            return StatusCodeWithPagination(HttpStatusCode.OK, pagedResponse);
        }

        [HttpPost] // TODO : BELUM DI TEST
        public async Task<IActionResult> Add(ProductDto request)
        {
            var product = _mapper.Map<Product>(request);
            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Add(product));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUpdateProductDto request)
        {
            // UNDONE : Ganti ProductDto.Tags ke type Array of String
            // Lalu jika di property tsb ada element nya, hanya boleh tambahkan yang belum ada
            var foundProduct = await _repo.GetById(id);

            if (foundProduct == null)
                throw new NotFoundException(nameof(Product), id);

            foundProduct = _mapper.Map(request, foundProduct);
            foundProduct.Id = id;

            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Update(foundProduct));
        }

        [Authorize]
        [HttpPut("{productId}/tag)")]
        public async Task<IActionResult> UpdateTag(int productId, [FromBody] string[] tags)
        {
            var foundProduct = await _repo.GetById(productId);
            if (foundProduct == null)
                throw new NotFoundException(nameof(Product), productId);

            var newTags = new List<Tag>();
            foreach (var tag in tags)
            {
                newTags.Add(new Tag { Id = tag });
            }
            foundProduct.Tags = foundProduct.Tags.Union(newTags).ToList();

            await _repo.Update(foundProduct);
            return StatusCodeWithObject(HttpStatusCode.OK, foundProduct.Tags.Select(t => t.Id));

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundProduct = await _repo.GetById(id);

            if (foundProduct == null)
                throw new NotFoundException(nameof(Product), id);

            await _repo.Delete(foundProduct);

            return Ok();
        }
    }
}
