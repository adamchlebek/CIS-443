using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopF2019.Models;

namespace ShopF2019.Controllers
{
    public class ShoppersController : Controller
    {
        private readonly ShopF2019Context _context;

        public ShoppersController(ShopF2019Context context)
        {
            _context = context;
        }

        // GET: Shoppers
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Shopper.ToListAsync());
        //}

        /// <summary>
        /// Searches for a shopper in the Shopper context by Email address.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var shoppers = from s in _context.Shopper
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                shoppers = shoppers.Where(s => s.Email.Equals(searchString));
            }
            return View(await shoppers.ToListAsync());
        }

        // GET: Shoppers/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var shopper = await _context.Shopper
        //        .FirstOrDefaultAsync(m => m.ShopperID == id);
        //    if (shopper == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(shopper);
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopper = await _context.Shopper
               .Include(s => s.Carts)                       // added after scaffolding
               .AsNoTracking()                              // added after scaffolding
               .FirstOrDefaultAsync(m => m.ShopperID == id)
               .ConfigureAwait(true);                       // added after scaffolding
            if (shopper == null)
            {
                return NotFound();
            }

            return View(shopper);
        }

        // GET: Shoppers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoppers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ShopperID,LastName,FirstName,Email")] Shopper shopper)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shopper);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(shopper);
        //}

        /// <summary>
        /// Creates a new shopper, but only if the Email address is unique. 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create([Bind("ShopperID,LastName,FirstName,Email")] Shopper shopper)
        {
            var shoppers = from s in _context.Shopper
                           select s;
            shoppers = shoppers.Where(s => s.Email.Equals(shopper.Email));
            Shopper temp = shoppers.FirstOrDefault();
            if (temp == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(shopper);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), "Shoppers", new { id = shopper.ShopperID });
                }
            }

            // The user needs to have a unique Email address. Goes back to the orginal view.
            return View(shopper);
        }


        // GET: Shoppers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopper = await _context.Shopper.FindAsync(id);
            if (shopper == null)
            {
                return NotFound();
            }
            return View(shopper);
        }

        // POST: Shoppers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShopperID,LastName,FirstName,Email")] Shopper shopper)
        {
            if (id != shopper.ShopperID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopperExists(shopper.ShopperID))
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
            return View(shopper);
        }

        // GET: Shoppers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopper = await _context.Shopper
                .FirstOrDefaultAsync(m => m.ShopperID == id);
            if (shopper == null)
            {
                return NotFound();
            }

            return View(shopper);
        }

        // POST: Shoppers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopper = await _context.Shopper.FindAsync(id);
            _context.Shopper.Remove(shopper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopperExists(int id)
        {
            return _context.Shopper.Any(e => e.ShopperID == id);
        }
    }
}
