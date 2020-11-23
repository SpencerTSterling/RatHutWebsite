using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatHutWebsite.Data;
using RatHutWebsite.Models;

namespace RatHutWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Add(int id, string prevUrl)
        {
            // Get product from the database
            Product p = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            List<Product> cartProducts = CartCookieHelper.GetCartProducts(_httpContext);
            CartCookieHelper.AddProductToCart(_httpContext, p);

            TempData["Message"] = $"{p.Title} was added to your cart";

            // Redirect back to previous page
            return Redirect(prevUrl);
        }

        public IActionResult Summary()
        {
            List<Product> cartProducts =
                CartCookieHelper.GetCartProducts(_httpContext);

            return View(cartProducts);
        }
    }
}
