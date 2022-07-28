using System.ComponentModel.DataAnnotations;

namespace Haulio.FarmFresh.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
