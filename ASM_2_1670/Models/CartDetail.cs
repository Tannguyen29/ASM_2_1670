using ASM_2_1670.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ASM_2_1670.Areas.Admin.Models;

namespace ASM_2_1670.Models
{
	public class CartDetail
	{
		[Key]
		public int CartDetailID { get; set; }
		public int Quantity { get; set; }

		// Foreign keys
		public int CartID { get; set; }
		public int ProductID { get; set; }

		// Navigation properties
		[ForeignKey("CartID")]
		public Cart? Carts { get; set; }

		[ForeignKey("ProductID")]
		public Product? Products { get; set; }
	}

}
