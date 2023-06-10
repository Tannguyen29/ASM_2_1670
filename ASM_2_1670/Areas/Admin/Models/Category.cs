using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Areas.Admin.Models
{
	public class Category
	{
		[Key]
		public int CategoryID { get; set; }
		public string? CategoryName { get; set; }

		// Navigation properties
		public ICollection<Product>? Products { get; set; }
	}
}
