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
            string password = "1234";
            string password2 = "9999";
            byte[] PasswordHashCust1, PasswordSaltCust1, PasswordHashCust2, PasswordSaltCust2, PasswordHashCust3, PasswordSaltCust3;
            CreatePasswordHash(password, out PasswordHashCust1, out PasswordSaltCust1);
            CreatePasswordHash(password2, out PasswordHashCust2, out PasswordSaltCust2);
            CreatePasswordHash(password2, out PasswordHashCust3, out PasswordSaltCust3);
            string a = Encoding.Default.GetString(PasswordHashCust1);
            var cust1 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Dell",
                LastName = "Velti",
                Email = "dellV@outlook.com",
                PasswordHash = PasswordHashCust1,
                PasswordSalt = PasswordSaltCust1,
                IsAdmin = false
            }).Entity;

            var cust2 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Barry",
                LastName = "Allen",
                Email = "barrya@fastmail.com",
                PasswordHash = PasswordHashCust2,
                PasswordSalt = PasswordSaltCust2,
                IsAdmin = true
            }).Entity;

            var cust3 = ctx.Customers.Add(new Customer()
            {
                FirstName = "Emily",
                LastName = "Kyle",
                Email = "EmilyK@gmail.com",
                PasswordHash = PasswordHashCust3,
                PasswordSalt = PasswordSaltCust3,
                IsAdmin = true
            }).Entity;
            ctx.SaveChanges();
            Order order1 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust2.Id,
                DeliveryAddress = "14994 Mandrake Way",
                DeliveryDate = DateTime.Now.AddDays(-4),
                OrderDate = DateTime.Now.AddDays(-7),
                TotalPrice = 150
            }
                ).Entity;
            Order order2 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust2.Id,
                DeliveryAddress = "8 Stuart Alley",
                DeliveryDate = DateTime.Now.AddDays(-20),
                OrderDate = DateTime.Now.AddDays(-24),
                TotalPrice = 100
            }
                ).Entity;
            Order order3 = ctx.Orders.Add(new Order()
            {
                CustomerId = cust3.Id,
                DeliveryAddress = "3 Shelley Drive",
                DeliveryDate = DateTime.Now.AddDays(-5),
                OrderDate = DateTime.Now.AddDays(-8),
                TotalPrice = 100
            }
            ).Entity;
            Product product1 = ctx.Products.Add(new Product()
            {
                Name = "Cat mug",
                Price = 75,
                TypeName = "Mugs",
                ImageUrl = "funnycatmug.jpg"

            }).Entity;
            Product product2 = ctx.Products.Add(new Product()
            {
                Name = "Dog mug",
                Price = 75,
                TypeName = "Mugs",
                ImageUrl = "whitedogmug.jpg"

            }).Entity;
            Product product3 = ctx.Products.Add(new Product()
            {
                Name = "Cat shirt",
                Price = 100,
                TypeName = "Tshirts",
                ImageUrl = "cutecattshirt.jpg"
            }).Entity;
            Product product4 = ctx.Products.Add(new Product()
            {
                Name = "Dog shirt",
                Price = 100,
                TypeName = "Tshirts",
                ImageUrl = "closedeyesdogtshirt.jpg"
            }).Entity;
            Product product5 = ctx.Products.Add(new Product()
            {
                Name = "Dog face mask",
                Price = 50,
                TypeName = "Other",
                ImageUrl = "browndogfacemask.jpg"

            }).Entity;
            Product product6 = ctx.Products.Add(new Product()
            {
                Name = "Cat keychain",
                Price = 30,
                TypeName = "Keychains",
                ImageUrl = "whitegreycatkeychain.jpg"

            }).Entity;
            Product product7 = ctx.Products.Add(new Product()
            {
                Name = "Dog keychain",
                Price = 35,
                TypeName = "Keychains",
                ImageUrl = "browndogkeychain.jpg"

            }).Entity;

            ctx.SaveChanges();

            OrderProduct ordProd1 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order1.Id,
                ProductId = product1.Id
            }).Entity;
            OrderProduct ordProd2 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order1.Id,
                ProductId = product2.Id
            }).Entity;
            OrderProduct ordProd3 = ctx.OrderProduct.Add(new OrderProduct()
            {
                OrderId = order2.Id,
                ProductId = product4.Id
            }).Entity;
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
