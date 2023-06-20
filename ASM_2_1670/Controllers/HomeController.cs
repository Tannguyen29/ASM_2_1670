using ASM_2_1670.Data;
using ASM_2_1670.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASM_2_1670.Controllers
{
    public class HomeController : Controller
    {
        private ASM_2_1670Context _contexts;

        public HomeController(ASM_2_1670Context context)
        {
            _contexts = context;
        }
        public IActionResult Index()
        {

            var _newProducts = _contexts.Product.Include(p => p.Category).OrderByDescending(p => p.CreatedDate).Take(10);
            var _hotProducts = _contexts.Product.Include(p => p.Category).OrderByDescending(p => p.ViewCount).Take(10);
            var model = new HomeViewModel { NewProducts = _newProducts.ToList(), HotProducts = _hotProducts.ToList() };
            return View(model);
        }

        [HttpGet]
        [Route("/SearchProduct")]
        public IActionResult SearchProduct(string searchString)
        {
			if (String.IsNullOrEmpty(searchString))
			{
				return View();
			}
			var searched_products = _contexts.Product.Include(prd => prd.Category)
                .Where(s => s.ProductName.Contains(searchString)); // It's always null
			ViewBag.searched_products = searched_products.ToList();
            return View();
		}


		[Route("/Product")]
        public IActionResult Product()
        {
            var _products = _contexts.Product.Include(p => p.Category);
            return View(_products.ToList());
        }

        [Route("/ProductByCategory")]
        public IActionResult ProductByCategory(int id)
        {
            var _products = _contexts.Product.Include(p => p.Category).Where(p => p.CategoryID == id);
            return View(_products.ToList());
        }



        [Route("/ProductDetails")]
        public async Task<IActionResult> ProductDetails(int? id)
        {            if (id == null || _contexts.Product == null)
            {
                return NotFound();
            }

            var product = await _contexts.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            // Increase view count
            product.ViewCount += 1;
            await _contexts.SaveChangesAsync();

            return View(product);
        }

    }
}