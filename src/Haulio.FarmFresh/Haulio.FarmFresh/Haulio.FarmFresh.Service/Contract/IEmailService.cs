using Haulio.FarmFresh.Domain.Settings;
using System.Threading.Tasks;

namespace Haulio.FarmFresh.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
