using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Areas.Admin.Models
{
	public class OrderDetail
	{
		[Key]
		public int OrderDetailID { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }

		// Foreign keys
		public int OrderID { get; set; }
		public int ProductID { get; set; }

		// Navigation properties
		[ForeignKey("OrderID")]
		public Order? Orders { get; set; }

		[ForeignKey("ProductID")]
		public Product? Product { get; set; }
	}
}
