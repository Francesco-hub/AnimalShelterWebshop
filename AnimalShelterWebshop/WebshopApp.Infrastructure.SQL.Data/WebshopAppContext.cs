using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data
{
    public class WebshopAppContext : DbContext
    {
        public WebshopAppContext(DbContextOptions<WebshopAppContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<OrderProduct>()
            //    .HasKey(op => new { op.OrderId, op.ProductId });
            //modelBuilder.Entity<OrderProduct>()
            //    .HasOne(o => o.Order)
            //    .WithMany(p => p.Products)
            //    .HasForeignKey(op => new { op.OrderId });
            //modelBuilder.Entity<OrderProduct>()
            //    .HasOne(p => p.Product)
            //    .WithMany(o => o.OrderProducts)
            //    .HasForeignKey(op => new { op.ProductId });
            //modelBuilder.Entity<Customer>()
            //   .HasMany(o => o.Orders);

            modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(p => p.Products)
            .UsingEntity<OrderProduct>(
                j => j
                    .HasOne(pt => pt.Order)
                    .WithMany(t => t.OrderProducts)
                    .HasForeignKey(pt => pt.OrderId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                   // j.Property(pt => pt.PublicationDate).HasDefaultValueSql("Quantity");
                    j.HasKey(t => new { t.OrderId, t.ProductId });
                });
            

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }



    }
}
