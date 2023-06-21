using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_2_1670.Areas.Admin.Models;
using ASM_2_1670.Data;
using ASM_2_1670.Models;

namespace ASM_2_1670.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ASM_2_1670Context _context;

        public OrdersController(ASM_2_1670Context context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        /*public async Task<IActionResult> Index()
        {
            var aSM_2_1670Context = _context.Order.Include(o => o.Carts).Include(o => o.Users);
            return View(await aSM_2_1670Context.ToListAsync());
        }*/
        public async Task<IActionResult> Index()
        {
            var aSM_2_1670Context = _context.Order.Include(o => o.Carts).Include(o => o.Users)
                                        .OrderByDescending(o => o.DateCreated);
            return View(await aSM_2_1670Context.ToListAsync());
        }


        /*        public async Task<IActionResult> Index(string searchString)
                {

                    var orders = from m in _context.Order
                                 select m;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        orders = orders.Where(s => s.OrderDetails.Contains(searchString));
                    }

                    return View(await orders.ToListAsync());
                }*/

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Carts)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["CartID"] = new SelectList(_context.Set<Cart>(), "CartID", "CartID");
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,DateCreated,TotalPrice,OrderStatus,UserID,CartID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartID"] = new SelectList(_context.Set<Cart>(), "CartID", "CartID", order.CartID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CartID"] = new SelectList(_context.Set<Cart>(), "CartID", "CartID", order.CartID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", order.UserID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,DateCreated,TotalPrice,OrderStatus,UserID,CartID")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartID"] = new SelectList(_context.Set<Cart>(), "CartID", "CartID", order.CartID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", order.UserID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Carts)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ASM_2_1670Context.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
