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

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(p => p.Products)
            .UsingEntity<OrderProduct>(
                j => j
                    .HasOne(pt => pt.Order)
                    .WithMany(t => t.OrderProducts)
                    .HasForeignKey(pt => pt.OrderId)
                    .OnDelete(DeleteBehavior.NoAction),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(pt => pt.ProductId)
                    .OnDelete(DeleteBehavior.NoAction), 
                j =>
                {
                    j.HasKey(t => new { t.OrderId, t.ProductId }); 
                });
            modelBuilder.Entity<Product>().Property<bool>("isDeleted");
            modelBuilder.Entity<Product>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);



        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

      /*  private void UpdateDeleted()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                Product prod = new Product();
                if(entry.Entity.GetType() == prod.GetType())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["isDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["isDeleted"] = true;
                            break;
                    }

                }

            }
        }
      */
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }



    }
}
