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
            string password = "1234";
            byte[] PasswordHashCust1, PasswordSaltCust1, PasswordHashCust2, PasswordSaltCust2, PasswordHashCust3, PasswordSaltCust3;
            CreatePasswordHash(password, out PasswordHashCust1, out PasswordSaltCust1);
            CreatePasswordHash(password, out PasswordHashCust2, out PasswordSaltCust2);
            CreatePasswordHash(password, out PasswordHashCust3, out PasswordSaltCust3);
            var cust1 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Mickey",
                LastName = "Mouse",
                Email = "Playhouse@Disneyland",
                PasswordHash = PasswordHashCust1,
                PasswordSalt = PasswordSaltCust1,
                IsAdmin = false
            }).Entity;

            var cust2 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Barry",
                LastName = "Allen",
                Email = "f",
                PasswordHash = PasswordHashCust2,
                PasswordSalt = PasswordSaltCust2,
                IsAdmin = true
            }).Entity;

            var cust3 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Wonder",
                LastName = "Woman",
                Email = "PCNuevoMolaMucho@TheBeibus.com",
                PasswordHash = PasswordHashCust3,
                PasswordSalt = PasswordSaltCust3,
                IsAdmin = true
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
                TypeName = "Mugs",
                ImageUrl = "ImageLink 1"

            }).Entity;
            Product product2 = ctx.Products.Add(new Product()
            {
                Name = "Cat Mug",
                Price = 8000,
                TypeName = "Mugs",
                ImageUrl = "ImageLink 2"

            }).Entity;
            Product product3 = ctx.Products.Add(new Product()
            {
                Name = "Panda Shirt",
                Price = 90,
                TypeName = "Tshirts",
                ImageUrl = "ImageLink 3"
            }).Entity;
            Product product4 = ctx.Products.Add(new Product()
            {
                Name = "Lion Shirt",
                Price = 90,
                TypeName = "Tshirts",
                ImageUrl = "ImageLink 4"
            }).Entity;
            Product product5 = ctx.Products.Add(new Product()
            {
                Name = "Epraldo",
                Price = 9000,
                TypeName = "Other",
                ImageUrl = "ImageLink 5"

            }).Entity;
            Product product6 = ctx.Products.Add(new Product()
            {
                Name = "Lele Keychain",
                Price = 5000,
                TypeName = "Keychains",
                ImageUrl = "ImageLink 6"

            }).Entity;
            Product product7 = ctx.Products.Add(new Product()
            {
                Name = "Yui Keychain",
                Price = 700,
                TypeName = "Keychains",
                ImageUrl = "ImageLink 7"

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

        private static void CreatePasswordHash(string password, out byte[] passwordHashCust, out byte[] passwordSaltCust)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSaltCust = hmac.Key;
                passwordHashCust = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
