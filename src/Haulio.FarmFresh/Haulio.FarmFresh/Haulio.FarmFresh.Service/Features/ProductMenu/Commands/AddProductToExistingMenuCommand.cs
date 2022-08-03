using Haulio.FarmFresh.Persistence;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.ProductMenuRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Features.ProductMenu.Commands
{
    public class AddProductToExistingMenuCommand : IRequest<Domain.Entities.ProductMenu>
    {
        public int Id { get; set; }
        public int[] ProductIds { get; set; }

        public class AddProductToExistingMenuCommandHandler : IRequestHandler<AddProductToExistingMenuCommand, Domain.Entities.ProductMenu>
        {
            private readonly ApplicationDbContext _context;

            public AddProductToExistingMenuCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Domain.Entities.ProductMenu> Handle(AddProductToExistingMenuCommand request, CancellationToken cancellationToken)
            {
                var existingMenu = _context.ProductMenus
                    .Include(pm => pm.Products)
                    .FirstOrDefault(pm => pm.Id == request.Id);

                if (existingMenu == null)
                    throw new NotFoundException(nameof(Domain.Entities.ProductMenu), request.Id);

                _context.ProductMenus.Attach(existingMenu);
                var foundProducts = new List<Domain.Entities.Product>();
                foreach (var prodId in request.ProductIds)
                {
                    var foundProduct = _context.Products.Find(prodId);
                    if (foundProduct == null)
                        throw new NotFoundException(nameof(Domain.Entities.Product), prodId);

                    if (existingMenu.Products.FirstOrDefault(pm => pm.Id == prodId) == null)
                    {
                        existingMenu.Products.Add(foundProduct);
                    }
                }
                await _context.SaveChangesAsync();
                return existingMenu;
            }
        }
    }
}