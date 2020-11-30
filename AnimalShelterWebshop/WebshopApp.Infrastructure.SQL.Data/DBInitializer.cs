using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data
{
    public class DBInitializer
    {
        public static void SeedDB(WebshopAppContext ctx)
        {
            //Every time I restart I reset the db: IMPORTANT: only on development process !!
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            var cust1 = ctx.Customers.Add(new Customer()
            {
                ID = 1,
                FirstName = "Mickey",
                LastName = "Mouse",
                Address = "Playhouse"
            }).Entity;

            ctx.Customers.Add(new Customer()
            {
                //ID = 2,
                FirstName = "Ceni",
                LastName = "Cienta",
                Address = "Castillo"
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });
            ctx.SaveChanges();
        }
    }
}
