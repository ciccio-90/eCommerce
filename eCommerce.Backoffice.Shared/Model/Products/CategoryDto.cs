using System.ComponentModel.DataAnnotations;

namespace eCommerce.Backoffice.Shared.Model.Products
{
    public class CategoryDto
    {
        [Display(ShortName = "Identifier")]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(ShortName = "Name")]
        public string Name { get; set; }
    }
}