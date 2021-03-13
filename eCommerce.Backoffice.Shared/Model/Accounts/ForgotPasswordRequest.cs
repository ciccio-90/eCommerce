using System.ComponentModel.DataAnnotations;

namespace eCommerce.Backoffice.Shared.Model.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
    }
}