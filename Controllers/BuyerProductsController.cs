using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAOnlineMart.Data;
using SAOnlineMart.Models;
using System.Linq;
using System.Threading.Tasks;
using SAOnlineMart.Extensions;
using System.Collections.Generic;
using System;

namespace SAOnlineMart.Controllers
{
    public class BuyerProductsController : Controller
    {
        private readonly SAOnlineMartContext _context;

        public BuyerProductsController(SAOnlineMartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("cart") ?? new List<Product>();
                cart.Add(product);
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("cart") ?? new List<Product>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("cart");
            if (cart != null)
            {
                var productToRemove = cart.FirstOrDefault(p => p.ProductID == productId);
                if (productToRemove != null)
                {
                    cart.Remove(productToRemove);
                    HttpContext.Session.SetObjectAsJson("cart", cart);
                }
            }
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("cart") ?? new List<Product>();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Product>>("cart");

            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty. Please add products to your cart before checking out.";
                return RedirectToAction(nameof(Checkout)); // Stay on the checkout page
            }

            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var order = new Order
            {
                UserID = userId.Value,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cart.Sum(p => p.Price),
                User = await _context.Users.FindAsync(userId),
                OrderItems = new List<OrderItem>()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var product in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = product.ProductID,
                    Quantity = 1, // Adjust based on your cart structure
                    Price = product.Price,
                    Order = order,
                    Product = product
                };

                _context.OrderItems.Add(orderItem);

                // Reduce stock
                product.StockQuantity -= 1;
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();

            // Clear cart
            HttpContext.Session.Remove("cart");

            TempData["SuccessMessage"] = "Order placed successfully!";

            return RedirectToAction(nameof(Index)); // Redirect to the buyer products page
        }

    }
}