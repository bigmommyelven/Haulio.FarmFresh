using Haulio.FarmFresh.Persistence;
using Haulio.FarmFresh.Service.Exceptions;
using Haulio.FarmFresh.Service.Repositories.ProductMenuRepository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Features.ProductMenu.Commands
{
    public class AddProductMenuCommand : IRequest<Domain.Entities.ProductMenu>
    {
        public int Position { get; set; }
        public string DisplayText { get; set; }
        public int[] ProductIds { get; set; }

        public class AddProductMenuCommandHandler : IRequestHandler<AddProductMenuCommand, Domain.Entities.ProductMenu>
        {
            private readonly IProductMenuRepository _repo;
            private readonly IApplicationDbContext _context;

            public AddProductMenuCommandHandler(IProductMenuRepository repo, IApplicationDbContext context)
            {
                _repo = repo;
                _context = context;
            }

            public async Task<Domain.Entities.ProductMenu> Handle(AddProductMenuCommand request, CancellationToken cancellationToken)
            {
                if (_context.ProductMenus.FirstOrDefault(pm => pm.Position == request.Position) != null)
                    throw new BadRequestException($"Menu with position {request.Position} already exists!");

                var newMenu = new Domain.Entities.ProductMenu
                {
                    DisplayText = request.DisplayText,
                    Position = request.Position,
                    IsActive = true,
                    Products = new List<Domain.Entities.Product>()
                };

                _context.ProductMenus.Attach(newMenu);

                foreach (var prodId in request.ProductIds)
                {
                    var foundProduct = _context.Products.Find(prodId);
                    if (foundProduct == null)
                        throw new NotFoundException(nameof(Domain.Entities.Product), prodId);

                    newMenu.Products.Add(foundProduct);
                }

                _repo.AddMenu(newMenu);
                await _repo.SaveChangesAsync();
                return newMenu;
            }
        }
    }
}
