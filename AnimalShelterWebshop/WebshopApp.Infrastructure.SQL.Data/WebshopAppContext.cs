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
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.OrderList)
                .OnDelete(DeleteBehavior.SetNull);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        // public DbSet<User> Users { get; set; }
    }
}
