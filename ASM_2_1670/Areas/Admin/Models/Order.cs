using ASM_2_1670.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Areas.Admin.Models
{
	public class Order
	{
		[Key]
		public int OrderID { get; set; }
		public DateTime DateCreated { get; set; }
		public double TotalPrice { get; set; }
		public string? OrderStatus { get; set; }

		// Foreign key
		public int UserID { get; set; }
		public int? CartID { get; set; } // Foreign key to Cart

		// Navigation properties
		[ForeignKey("UserID")]
		public User? Users { get; set; }
		public Cart? Carts { get; set; } // Navigation property to Cart
        
		public List<OrderDetail> OrderDetails  = new List<OrderDetail>();
        //public Payment Payment { get; set; }

    }
}
