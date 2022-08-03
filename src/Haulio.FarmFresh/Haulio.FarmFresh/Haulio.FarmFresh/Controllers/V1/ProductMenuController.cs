using Haulio.FarmFresh.Service.Features.ProductMenu.Commands;
using Haulio.FarmFresh.Service.Features.ProductMenu.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Controllers.V1
{
    public class ProductMenuController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductMenuController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductMenus()
        {
            var productMenus = await _mediator.Send(new GetProductMenusQuery());


            var resultObject = productMenus
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
                }).ToList();

            return StatusCodeWithObject(System.Net.HttpStatusCode.OK, resultObject);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenu(AddProductMenuCommand command)
        {
            var newMenu = await _mediator.Send(command);
            return StatusCodeWithObject(System.Net.HttpStatusCode.OK, newMenu);
        }

        [HttpPost("{menuId}/product")]
        public async Task<IActionResult> AddProductToMenu(int menuId, [FromBody]int[] productIds)
        {
            var command = new AddProductToExistingMenuCommand
            {
                Id = menuId,
                ProductIds = productIds
            };
            var newMenu = await _mediator.Send(command);
            return StatusCodeWithObject(System.Net.HttpStatusCode.OK, newMenu);
        }
    }
}
