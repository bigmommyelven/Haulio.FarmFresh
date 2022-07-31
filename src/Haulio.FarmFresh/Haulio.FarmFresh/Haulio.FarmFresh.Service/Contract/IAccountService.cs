using Haulio.FarmFresh.Domain.Auth;
using Haulio.FarmFresh.Domain.Common;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Contract
{
    public interface IAccountService
    {
        Task<Response> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    }
}
