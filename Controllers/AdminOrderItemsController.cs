using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAOnlineMart.Data;
using SAOnlineMart.Models;

namespace SAOnlineMart.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderItemsController : Controller
    {
        private readonly SAOnlineMartContext _context;

        public AdminOrderItemsController(SAOnlineMartContext context)
        {
            _context = context;
        }

        // GET: AdminOrderItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderItems.Include(o => o.Product).ToListAsync());
        }

        // GET: AdminOrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var orderItem = await _context.OrderItems
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderItemID == id);
            if (orderItem == null)
                return NotFound();

            return View(orderItem);
        }

        // GET: AdminOrderItems/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            return View();
        }

        // POST: AdminOrderItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemID,OrderID,ProductID,Quantity,Price")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: AdminOrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
                return NotFound();

            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", orderItem.ProductID);
            return View(orderItem);
        }

        // POST: AdminOrderItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderItemID,OrderID,ProductID,Quantity,Price")] OrderItem orderItem)
        {
            if (id != orderItem.OrderItemID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OrderItemID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: AdminOrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var orderItem = await _context.OrderItems
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderItemID == id);
            if (orderItem == null)
                return NotFound();

            return View(orderItem);
        }

        // POST: AdminOrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderItemID == id);
        }
    }
}
