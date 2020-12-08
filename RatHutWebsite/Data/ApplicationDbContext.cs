using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RatHutWebsite.Models;

namespace RatHutWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet for products - menu items 
        public DbSet<Product> Products { get; set; }

        // DbSet for orders
        public DbSet<Order> Orders { get; set; }
    }
}
