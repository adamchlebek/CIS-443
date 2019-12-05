using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopF2019.Models;

/// <summary>
///  ---------------------------------------------------------------------------------
/// Methods in the CartsController to fill in code to define the required actions
///  ---------------------------------------------------------------------------------
/// 1) Details to include the Orders in the cart followed by the product in the order
/// 2) Create no new cart, because existing cart not checked out
/// 3) Create to create a new cart
/// 4) Edit to edit the cart for the purpose to check out the shopper
///  ---------------------------------------------------------------------------------
/// </summary>

namespace ShopF2019.Controllers
{
    public class CartsController : Controller
    {
        private readonly ShopF2019Context _context;

        public CartsController(ShopF2019Context context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var shopF2019Context = _context.Cart.Include(c => c.Shopper);
            return View(await shopF2019Context.ToListAsync());
        }

        // GET: Carts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Cart
        //        .Include(c => c.Shopper)
        //        .FirstOrDefaultAsync(m => m.CartID == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

            /// <summary>
            /// 1)
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(a => a.Shopper)
                // added after scaffolding
                // added after scaffolding
                .AsNoTracking()                             // added after scaffolding
                .FirstOrDefaultAsync(m => m.CartID == id)
                .ConfigureAwait(true);                      // added after scaffolding
            if (cart == null)
            {
                return NotFound();
            }

            ViewData["CartID"] = id;
            return View(cart);
        }

        // GET: Carts/Create
        //public IActionResult Create()
        //{
        //    ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "Email");
        //    return View();
        //}
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                int num = (int)id;

                var temp = _context.Cart
                    .Include(c => c.Shopper)
                    .AsNoTracking();

                // 2) detect that the shopper cart is not checked out



                return RedirectToAction(nameof(Details), "Carts", new { id = ____.CartID });

            }

            ViewData["ShopperID"] = id;
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CartID,ShopperID,TimeSlot,Subtotal,Tax,TotalCost,CheckedOut")] Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(cart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "Email", cart.ShopperID);
        //    return View(cart);
        //}

        public async Task<IActionResult> Create([Bind("CartID,ShopperID,TimeSlot,CheckedOut")] Cart cart)
        {
            // 3) Initialize the properties of the cart






            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Carts", new { id = cart.CartID });
            }
            ViewData["ShopperID"] = cart.ShopperID;
            return View(cart);
        }

        // GET: Carts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Cart.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "Email", cart.ShopperID);
        //    return View(cart);
        //}

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var temp = await _context.Cart
                .Include(a => a.Orders)
                .ThenInclude(a => a.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CartID == id)
                .ConfigureAwait(true);
            if (temp.CheckedOut)
            {
                return RedirectToAction(nameof(Index));
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "FirstName", cart.ShopperID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CartID,ShopperID,TimeSlot,Subtotal,Tax,TotalCost,CheckedOut")] Cart cart)
        //{
        //    if (id != cart.CartID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(cart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CartExists(cart.CartID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "Email", cart.ShopperID);
        //    return View(cart);
        //}

        // The Edit action is used to check out the shopper cart.
        public async Task<IActionResult> Edit(int id, [Bind("CartID,ShopperID")] Cart cart)
        {
            if (id != cart.CartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                // 4) Include the required code to checkout the cart.

































                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "Carts", new { id = cart.CartID });
            }
            ViewData["ShopperID"] = new SelectList(_context.Shopper, "ShopperID", "FirstName", cart.ShopperID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Shopper)
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartID == id);
        }
    }
}
