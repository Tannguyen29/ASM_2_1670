using ASM_2_1670.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM_2_1670.Models
{
	public class Payment
	{
		[Key]
		public int PaymentID { get; set; }
		public DateTime PaymentDate { get; set; }
		public double Amount { get; set; }
		public string? PaymentMethod { get; set; }

		// Foreign key
		public int OrderID { get; set; }

		// Navigation property
		[ForeignKey("OrderID")]
		public Order? Orders { get; set; }
	}
}
