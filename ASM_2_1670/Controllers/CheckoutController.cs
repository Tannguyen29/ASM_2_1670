using System;
using ASM_2_1670.Data;
using ASM_2_1670.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ASM_2_1670.Areas.Admin.Models;


namespace ASM_2_1670.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DbContextOptions<ASM_2_1670Context> _contextOptions;

        private readonly ASM_2_1670Context _context;

        public CheckoutController(ASM_2_1670Context context, DbContextOptions<ASM_2_1670Context> contextOptions)
        {
            _context = context;
            _contextOptions = contextOptions;
        }

        // GET: Checkout
        public ActionResult Index()
        {
/*            var order = new Order();*/
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(User user, Order order)
        {
            // Lưu thông tin người dùng vào cơ sở dữ liệu hoặc thực hiện các thao tác khác
            // Ví dụ:
            var dbContext = _context;
            dbContext.User.Add(user);
            dbContext.SaveChanges();

            // Xử lý đơn hàng
            // Tạo ngày đặt hàng
            order.DateCreated = DateTime.Now;
            // Tính tổng giá trị đơn hàng
            double totalPrice = CalculateTotalPrice(order.OrderDetails);
            order.TotalPrice = totalPrice;
            // Lưu đơn hàng vào cơ sở dữ liệu
            dbContext.Order.Add(order);
            dbContext.SaveChanges();

            // Trừ đi stock trong sản phẩm
            foreach (var orderDetail in order.OrderDetails)
            {
                var product = dbContext.Product.Find(orderDetail.ProductID);
                if (product != null)
                {
                    product.Stock -= orderDetail.Quantity;
                    dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Confirmation");
        }

        // Tính tổng giá trị đơn hàng
        private double CalculateTotalPrice(List<OrderDetail> orderDetails)
        {
            double totalPrice = 0;
            foreach (var orderDetail in orderDetails)
            {
                totalPrice += orderDetail.Price * orderDetail.Quantity;
            }
            return totalPrice;
        }


        [Route("Confirmation")]
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}
