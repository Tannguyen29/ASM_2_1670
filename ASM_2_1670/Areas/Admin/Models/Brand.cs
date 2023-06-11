using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Areas.Admin.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        public string? BrandName { get; set; }

        [Required]
        public string? BrandDescription { get; set;} 

        public int BrandOrder { get; set; }

        public ICollection<Product>? products { get; set; }
    }
}
