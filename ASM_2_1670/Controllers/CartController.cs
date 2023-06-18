using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASM_2_1670.Data;
using ASM_2_1670.Areas.Admin.Models;
using ASM_2_1670.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASM_2_1670.Controllers
{
    public class CartController : Controller
    {
        private readonly ASM_2_1670Context _context;

        public CartController(ASM_2_1670Context context)
        {
            _context = context;
        }

        // Method to add a product to the cart.
        [Route("/AddToCart")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Check if user is logged in
            if (User.Identity.IsAuthenticated)
            {
                // Get the user
                var user = await _context.User.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);

                if (user != null)
                {
                    // Check if the cart already exists
                    var cart = await _context.Cart.Include(c => c.CartDetails)
                        .FirstOrDefaultAsync(c => c.UserID == user.UserId);

                    // If the cart does not exist, create a new one
                    if (cart == null)
                    {
                        cart = new Cart
                        {
                            UserID = user.UserId,
                            CartDetails = new List<CartDetail>()
                        };
                        _context.Cart.Add(cart);
                    }

                    // Check if the product already exists in the cart
                    var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductID == productId);

                    if (cartDetail != null)
                    {
                        // If the product already exists in the cart, increase the quantity
                        cartDetail.Quantity += quantity;
                    }
                    else
                    {
                        // If the product does not exist in the cart, add it
                        cartDetail = new CartDetail
                        {
                            CartID = cart.CartID,
                            ProductID = productId,
                            Quantity = quantity
                        };
                        cart.CartDetails.Add(cartDetail);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction("CartDetails", "Cart");
                }
            }

            // If user is not authenticated, redirect them to the Login page
            return RedirectToAction("Login", "Account");
        }

        [Route("/CartDetails")]
		public IActionResult CartDetails()
		{
			// Get the currently logged-in user
			var user = _context.User.FirstOrDefault(u => u.UserEmail == User.Identity.Name);

			if (user != null)
			{
				// Get the cart for the user
				var cart = _context.Cart.Include(c => c.CartDetails).ThenInclude(cd => cd.Products)
					.FirstOrDefault(c => c.UserID == user.UserId);

				if (cart != null)
				{
					ViewBag.CartQuantity = cart.CartDetails.Sum(cd => cd.Quantity);
					return View(cart);
				}
			}

			// If there is no cart or user, you can handle the scenario as needed, such as displaying an empty cart view or an error message.
			ViewBag.CartQuantity = 0;
			return PartialView("_EmptyCart");
		}

		[Route("/Cart/GetCartItems")]
		public IActionResult GetCartItems()
		{
			// Lấy danh sách sản phẩm trong giỏ hàng
			var user = _context.User.FirstOrDefault(u => u.UserEmail == User.Identity.Name);

			if (user != null)
			{
				var cart = _context.Cart.Include(c => c.CartDetails).ThenInclude(cd => cd.Products)
					.FirstOrDefault(c => c.UserID == user.UserId);

				if (cart != null)
				{
					return PartialView("_CartItems", cart.CartDetails);
				}
			}

			// Nếu không có giỏ hàng hoặc người dùng, trả về một phần tử HTML trống
			return PartialView("_EmptyCart");
		}


	}
}
