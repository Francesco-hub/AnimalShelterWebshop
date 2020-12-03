using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data
{
    public class WebshopAppContext : DbContext
    {
        public WebshopAppContext(DbContextOptions<WebshopAppContext> opt): base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            // Opción A = Order tiene Customer
            //
            //modelBuilder.Entity<Order>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(e => e.OrderList)
            //    .OnDelete(DeleteBehavior.SetNull);

            //
            // Opción B = Order tiene CustomerID
            //
            //modelBuilder.Entity<Customer>()
            //   .OwnsMany(p => p.OrderList);

            //modelBuilder.Entity<Order>()
            //  .OwnsMany(r => r.ProductList);
          

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

      

    }
}
