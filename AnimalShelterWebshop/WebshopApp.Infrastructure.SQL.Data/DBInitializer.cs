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
            Product product1 = ctx.Products.Add(new Product()
            {
                ID = 1,
                Name = "Taza de It",
                Price = 3000,
                TypeName = "Taza"

            }).Entity;
            Product product2 = ctx.Products.Add(new Product()
            {
                ID = 2,
                Name = "Taza de Lele",
                Price = 8000,
                TypeName = "Taza"

            }).Entity;
            Product product3 = ctx.Products.Add(new Product()
            { ID = 3,
                Name = "Camiseta de Pando",
                Price = 90,
                TypeName = "Camiseta"
            }).Entity;
            Product product4 = ctx.Products.Add(new Product()
            {
                ID = 4,
                Name = "Camiseta de Simbito",
                Price = 90,
                TypeName = "Camiseta"
            }).Entity;
            Order order1 = ctx.Orders.Add(new Order()
            {
                ID = 1,
                CustomerID = 1,
                DeliveryAddress = "La casa de Dron",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 300,
                ProductList =new List<Product> { product1, product2 }

            }
                ).Entity;
            Order order2 = ctx.Orders.Add(new Order()
            {
                ID = 2,
                CustomerID = 18,
                DeliveryAddress = "La casa los cuscurros",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 302,
                ProductList = new List<Product> { product1, product3 }

            }
                ).Entity;
            //Order order3 = ctx.Orders.Add(new Order()
            //{
            //    ID = 3,
            //    CustomerID = 55,
            //    DeliveryAddress = "La casa de simbo",
            //    DeliveryDate = DateTime.Now,
            //    OrderDate = DateTime.Now,
            //    TotalPrice = 310,
            //    ProductList = new List<Product> { product2, product3 }

            //}
            //    ).Entity;
            Order order4 = ctx.Orders.Add(new Order()
            {
                ID = 4,
                CustomerID = 1408,
                DeliveryAddress = "La casa de simbo",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 310,
                ProductList = new List<Product> { product2, product4 }

            }
               ).Entity;
            var cust1 = ctx.Customers.Add(new Customer()
            {
                ID = 1,
                FirstName = "Mickey",
                LastName = "Mouse",
                Email = "Playhouse@Disneyland",
                Password = "Ch",
                OrderList = new List<Order>
                { 
                    order1,order2
                }
               
            }).Entity;

            var cust2 = ctx.Customers.Add(new Customer()
            {
                ID = 2,
                FirstName = "Barry",
                LastName = "Allen",
                Email = "FastMail@star-labs.cc",
                Password = "run",
                OrderList = new List<Order>
                {
                    order1,order4
                }
            }).Entity;

            var cust3 = ctx.Customers.Add(new Customer()
            {
                ID = 3,
                FirstName = "Wonder",
                LastName = "Woman",
                Email = "PCNuevoMolaMucho@TheBeibus.com",
                Password = "Epraldo",
                OrderList = new List<Order>
                {
                    order2,order4
                }
            }).Entity;
            ctx.SaveChanges();
        }
    }
}
