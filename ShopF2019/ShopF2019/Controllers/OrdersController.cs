using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopF2019.Models;

/// <summary>
///  -----------------------------------------------------------------------------
/// Methods in the OrdersController to fill in code to define the required actions
///  -----------------------------------------------------------------------------
/// 1) Create to create or to modify an order
/// 2) Edit to edit an existing order
/// 3) Delete to delete an existing order
///  -----------------------------------------------------------------------------
/// </summary>

namespace ShopF2019.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShopF2019Context _context;

        public OrdersController(ShopF2019Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var shopF2019Context = _context.Order.Include(o => o.Cart).Include(o => o.Product);
            return View(await shopF2019Context.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Cart)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID");
        //    ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID");
        //    return View();
        //}
        public IActionResult Create(int? id)
        {
            ViewData["CartID"] = id;
            ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderID,CartID,ProductID,Quantity,Cost")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", order.CartID);
        //    ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID", order.ProductID);
        //    return View(order);
        //}

        public async Task<IActionResult> Create([Bind("OrderID,CartID,ProductID,Quantity")] Order order)
        {
            Cart cart = _context.Cart.FirstOrDefault(m => m.CartID == order.CartID);
            if (cart.CheckedOut)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {

                // 1) Include the required code to create, or modify, an order


































                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Carts", new { id = order.CartID });
            }
            ViewData["CartID"] = order.CartID;
            ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID", order.ProductID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", order.CartID);
            ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID", order.ProductID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CartID,ProductID,Quantity")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Cart cart = _context.Cart.FirstOrDefault(m => m.CartID == order.CartID);

                if (cart.CheckedOut)
                {
                    return RedirectToAction(nameof(Index));
                }

               // 2) Include the required code to edit an order























                try
                {
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
                 return RedirectToAction(nameof(Details), "Carts", new { id = order.CartID });
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", order.CartID);
            ViewData["ProductID"] = new SelectList(_context.Set<Product>(), "ProductID", "ProductID", order.ProductID);
            return View(order);
        }

        // GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .Include(o => o.Cart)
        //        .Include(o => o.Product)
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Cart)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            var cart = order.Cart;
            if (cart.CheckedOut)
            {
                return RedirectToAction(nameof(Details), "Carts", new { id = order.CartID });
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Order.FindAsync(id);
        //    _context.Order.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order
                
            // 3) Include the required code to edit an order









            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Carts", new { id = order.CartID });
         }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
