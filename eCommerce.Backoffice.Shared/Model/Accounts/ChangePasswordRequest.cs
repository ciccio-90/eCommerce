using System.ComponentModel.DataAnnotations;

namespace eCommerce.Backoffice.Shared.Model.Accounts
{
    public class ChangePasswordRequest
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}