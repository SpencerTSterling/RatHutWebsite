using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RatHutWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatHutWebsite
{
    public static class CartCookieHelper
    {
        const string CartCookie = "ShoppingCartCookie";
        public static List<Product> GetCartProducts(IHttpContextAccessor http)
        {
            // Get existing cart items
            string existingItems =
                http.HttpContext.Request.Cookies[CartCookie];

            List<Product> cartProducts = new List<Product>();
            if (existingItems != null)
            {
                cartProducts =
                    JsonConvert.DeserializeObject<List<Product>>(existingItems);
            }
            return cartProducts;
        }

        public static void AddProductToCart(IHttpContextAccessor http, Product p)
        {
            List<Product> cartProducts = GetCartProducts(http);
            cartProducts.Add(p);
            // Add product to cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        }

        public static void EmptyCart(IHttpContextAccessor http)
        {
            http.HttpContext.Response.Cookies.Delete(CartCookie);
        }
    }
}
