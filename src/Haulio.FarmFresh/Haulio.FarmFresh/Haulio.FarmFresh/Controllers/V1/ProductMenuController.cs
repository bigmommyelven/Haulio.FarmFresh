using Haulio.FarmFresh.Service.Repositories.ProductMenuRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    public class ProductMenuController : BaseController
    {
        private readonly IProductMenuRepository _repo;

        public ProductMenuController(IProductMenuRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductMenus()
        {
            var entList = await _repo.GetProductMenu();

            var resultObject = entList
                .Select(pm => new
                {
                    pm.Id,
                    pm.Position,
                    pm.DisplayText,
                    pm.IsActive,
                    Products = pm.Products.Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Description,
                        p.CategoryId,
                        p.Price,
                        p.Strategy,
                        ImageUrls = p.ProductImages.Select(pi => pi.ImageUrl).ToArray()
                    })
                });

            return StatusCodeWithObject(System.Net.HttpStatusCode.OK, resultObject);
        }
    }
}
