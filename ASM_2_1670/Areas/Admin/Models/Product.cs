using ASM_2_1670.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Areas.Admin.Models
{
	public class Product
	{
		[Key]
		public int ProductID { get; set; }
		public string? ProductName { get; set; }
		public string? ProductDescription { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }
		public string? ImageURL { get; set; }

		// Foreign key
		public int? CategoryID { get; set; }

		// Navigation properties
		[ForeignKey("CategoryID")]
		public Category? Categories { get; set; }
		public ICollection<CartDetail>? CartDetails { get; set; }
		public ICollection<OrderDetail>? OrderDetails { get; set; }

	}
}
