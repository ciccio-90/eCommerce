using System.ComponentModel.DataAnnotations;

namespace eCommerce.Backoffice.Shared.Model.Roles
{
    public class RoleDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; }
    }
}