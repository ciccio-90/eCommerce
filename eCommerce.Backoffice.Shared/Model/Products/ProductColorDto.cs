using System.ComponentModel.DataAnnotations;

namespace eCommerce.Backoffice.Shared.Model.Products
{
    public class ProductColorDto
    {
        [Display(ShortName = "Identifier")]
        public long Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(ShortName = "Name")]
        public string Name { get; set; }
    }
}