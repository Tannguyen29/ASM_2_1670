using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ASM_2_1670.Areas.Admin.Models;

namespace ASM_2_1670.Models
{
    public class Cart
	{
		[Key]
		public int CartID { get; set; }

		// Foreign key
		public int UserID { get; set; }

		// Navigation properties
		[ForeignKey("UserID")]
		public User? Users { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; } = new List<CartDetail>();
    }
}
