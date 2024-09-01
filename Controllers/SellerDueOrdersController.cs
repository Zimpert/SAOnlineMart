using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAOnlineMart.Data;
using SAOnlineMart.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SAOnlineMart.Controllers
{
    public class SellerDueOrdersController : Controller
    {
        private readonly SAOnlineMartContext _context;

        public SellerDueOrdersController(SAOnlineMartContext context)
        {
            _context = context;
        }

        // GET: SellerDueOrders
        public async Task<IActionResult> Index()
        {
            var sellerID = HttpContext.Session.GetInt32("UserID");

            var orderItems = await _context.OrderItems
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .ThenInclude(o => o.User) // Include the User to access the buyer's address
                .Where(oi => oi.Product.SellerID == sellerID)
                .Select(oi => new SellerOrderItemViewModel
                {
                    OrderID = oi.OrderID,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    BuyerAddress = oi.Order.User.Address,
                    OrderDate = oi.Order.OrderDate,
                    Status = oi.Order.Status
                })
                .ToListAsync();

            return View(orderItems);
        }

        // GET: SellerDueOrders/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User) // Include User to access the buyer's address
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null || !order.OrderItems.Any(oi => oi.Product.SellerID == HttpContext.Session.GetInt32("UserID")))
            {
                return NotFound();
            }

           
            var firstOrderItem = order.OrderItems.FirstOrDefault();
            if (firstOrderItem == null)
            {
                return NotFound(); // Handle case where there are no order items
            }

            var viewModel = new SellerOrderItemViewModel
            {
                OrderID = order.OrderID,
                ProductName = firstOrderItem.Product.Name, // Ensure ProductName is set
                Status = order.Status,
                BuyerAddress = order.User.Address,
                OrderDate = order.OrderDate,
                Quantity = firstOrderItem.Quantity, 
                Price = firstOrderItem.Price 
                                             
            };

            return View(viewModel);
        }

        // POST: SellerDueOrders/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,Status")] SellerOrderItemViewModel viewModel)
        {
            if (id != viewModel.OrderID)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            // Ensure that OrderItems is not null
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                return NotFound("No order items found for this order.");
            }

            // Check if the seller has the right to modify the order
            if (!order.OrderItems.Any(oi => oi.Product.SellerID == HttpContext.Session.GetInt32("UserID")))
            {
                return Unauthorized();
            }

            order.Status = viewModel.Status;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Orders.Any(e => e.OrderID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(viewModel);
        }
    }
}
