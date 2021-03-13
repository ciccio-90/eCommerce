using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Backoffice.Shared.Model.Products
{
    [Table("Categories")]
    public class Category
    {
        [Column("CategoryId")]
        [Key]
        [Display(ShortName = "Identifier")]
        public int Id { get; set; }

        [Column("Name")]
        [StringLength(50)]
        [Required]
        [Display(ShortName = "Name")]
        public string Name { get; set; }
    }
}