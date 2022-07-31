using AutoMapper;
using Haulio.FarmFresh.Domain.Common;
using Haulio.FarmFresh.Domain.Entities;
using Haulio.FarmFresh.Infrastructure.Dto;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ent = await _repo.GetById(id);
            var category = _mapper.Map<CategoryDto>(ent);
            return StatusCodeWithObject(HttpStatusCode.OK, category);
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
        public async Task<IActionResult> Add(CategoryDto request)
        {

            var category = _mapper.Map<Category>(request);
            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Add(category));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto request)
        {
            var foundCategory = await _repo.GetById(id);

            if (foundCategory == null)
                throw new NotFoundException(nameof(Category), id);

            foundCategory = _mapper.Map(request, foundCategory);
            foundCategory.Id = id;

            return StatusCodeWithObject(HttpStatusCode.OK, await _repo.Update(foundCategory));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundCategory = await _repo.GetById(id);

            if (foundCategory == null)
                throw new NotFoundException(nameof(Category), id);

            await _repo.Delete(foundCategory);

            return Ok();
        }

        [HttpGet("{id}/product")]
        public async Task<IActionResult> GetCategoryProducts(int id)
        {
            var res = await _repo.GetCategoryProducts(id);
            return Ok(res);
        }

        [HttpGet("{id}/product/{productId}")]
        public async Task<IActionResult> GetCategoryProductById(int id, int productId)
        {
            var res = await _repo.GetCategoryProductById(id, productId);
            return StatusCodeWithObject(HttpStatusCode.OK, res);
        }
    }
}
