using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAOnlineMart.Data;
using SAOnlineMart.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SAOnlineMart.Controllers
{
    public class SellerProductsController : Controller
    {
        private readonly SAOnlineMartContext _context;

        public SellerProductsController(SAOnlineMartContext context)
        {
            _context = context;
        }

        // GET: SellerProducts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sellerID = HttpContext.Session.GetInt32("UserID");
 
            var products = await _context.Products
                .Where(p => p.SellerID == sellerID)
                .ToListAsync();
            return View(products);
        }

        // GET: SellerProducts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SellerProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Description,Price,StockQuantity,ImageURL")] Product product)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var sellerID))
            {
                return RedirectToAction("Login", "Users"); // Redirect to login if UserID is not set or invalid
            }

            product.SellerID = sellerID;

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: SellerProducts/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var sellerID))
            {
                return RedirectToAction("Login", "Users"); // Redirect to login if UserID is not set or invalid
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null || product.SellerID != sellerID)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: SellerProducts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Description,Price,StockQuantity,ImageURL")] Product product)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var sellerID))
            {
                return RedirectToAction("Login", "Users"); // Redirect to login if UserID is not set or invalid
            }

            if (id != product.ProductID || product.SellerID != sellerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }

        // GET: SellerProducts/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var sellerID))
            {
                return RedirectToAction("Login", "Users"); // Redirect to login if UserID is not set or invalid
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id && m.SellerID == sellerID);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: SellerProducts/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userIdStr = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out var sellerID))
            {
                return RedirectToAction("Login", "Users"); // Redirect to login if UserID is not set or invalid
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null && product.SellerID == sellerID)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
