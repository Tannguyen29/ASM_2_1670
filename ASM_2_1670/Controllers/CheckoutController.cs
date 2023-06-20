using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASM_2_1670.Data;
using ASM_2_1670.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using ASM_2_1670.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_2_1670.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ASM_2_1670Context _context;

        public CheckoutController(ASM_2_1670Context context)
        {
            _context = context;
        }

        // GET: Checkout
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserEmail == User.Identity.Name);

            var cart = await _context.Cart
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Products)
                .FirstOrDefaultAsync(m => m.UserID == user.UserId);

            if (cart == null)
            {
                // User does not have a cart, redirect them somewhere appropriate
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(cart);
        }

        // POST: Checkout

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([Bind("CartID,UserID")] Cart cart, bool SaveAddress = false)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserEmail == User.Identity.Name);

            if (ModelState.IsValid)
            {
                var cardDetails = await _context.CartDetail.Include(cd => cd.Products)
                    .Where(cd => cd.CartID == cart.CartID)
                    .ToListAsync();

                cart.CartDetails = cardDetails;

                var totalOrderPrice = cart.CartDetails.Sum(cd => cd.Quantity * cd.Products.Price);
                // Create new Order
                var order = new Order
                {
                    DateCreated = DateTime.Now,
                    UserID = user.UserId,
                    TotalPrice = totalOrderPrice,
                    OrderStatus = "Pending",
                    CartID = cart.CartID
                };

                _context.Add(order);
                await _context.SaveChangesAsync();

                

                foreach (var cartDetail in cart.CartDetails)
                {
                    // Create OrderDetail for each CartDetail
                    var orderDetail = new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ProductID = cartDetail.ProductID,
                        Quantity = cartDetail.Quantity,
                        Price = cartDetail.Products.Price,
                        Orders = order,
                        Product = cartDetail.Products
                    };

                    _context.Add(orderDetail);

                    // Decrease the product stock
                    var product = await _context.Product.FindAsync(cartDetail.ProductID);
                    if (product != null)
                    {
                        product.Stock -= cartDetail.Quantity;
                        _context.Product.Update(product);
                    }
                    
                }

                // Clear Cart
                _context.CartDetail.RemoveRange(cart.CartDetails);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(CheckoutController.Confirmation));
            }

            return View(cart);
        }



        [Route("/Confirmation")]
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}



