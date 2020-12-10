﻿using System;
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
                FirstName = "Mickey",
                LastName = "Mouse",
                Email = "Playhouse@Disneyland",
                Password = "Ch"
            }).Entity;

            var cust2 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Barry",
                LastName = "Allen",
                Email = "FastMail@star-labs.cc",
                Password = "run"
            }).Entity;

            var cust3 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Wonder",
                LastName = "Woman",
                Email = "PCNuevoMolaMucho@TheBeibus.com",
                Password = "Epraldo"
            }).Entity;
            ctx.SaveChanges();
            Order order1 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust1.Id,
                DeliveryAddress = "House 1",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 300
            }
                ).Entity;
            Order order2 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust1.Id,
                DeliveryAddress = "House 2",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 302
            }
                ).Entity;
            Order order3 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust3.Id,
                DeliveryAddress = "House 4",
                DeliveryDate = DateTime.Now,
                OrderDate = DateTime.Now,
                TotalPrice = 310
            }
            ).Entity;
            Product product1 = ctx.Products.Add(new Product()
            {
                Name = "It Mug",
                Price = 3000,
                TypeName = "Taza"

            }).Entity;
            Product product2 = ctx.Products.Add(new Product()
            {
                Name = "Cat Mug",
                Price = 8000,
                TypeName = "Taza"

            }).Entity;
            Product product3 = ctx.Products.Add(new Product()
            {
                Name = "Panda Shirt",
                Price = 90,
                TypeName = "Camiseta"
            }).Entity;
            Product product4 = ctx.Products.Add(new Product()
            {
                Name = "Lion Shirt",
                Price = 90,
                TypeName = "Camiseta"
            }).Entity;

            ctx.SaveChanges();

            OrderProduct ordProd1 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order1.Id,
                ProductId =  product1.Id
            }).Entity;
            OrderProduct ordProd2 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order1.Id,
                ProductId = product2.Id
            }).Entity;
            OrderProduct ordProd3 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order3.Id,
                ProductId = product4.Id
            }).Entity;
            ctx.SaveChanges();
            product2.OrderProducts.Add(ordProd2);
            product2.Orders.Add(order1);
            ctx.SaveChanges();
        }
    }
}
