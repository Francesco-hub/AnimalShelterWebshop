using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data
{
    public class SQLDbInitializer
    {
        public static void SeedDb(WebshopAppContext ctx)
        {
            ctx.Database.EnsureCreated();

            if (ctx.Customers.Any())
            {
                ctx.Database.ExecuteSqlRaw("DROP TABLE Customers");
                ctx.Database.EnsureCreated();
            }

            //Create customers with hashed and salter passwords
            string password = "1234";
            byte[] PasswordHashCust1, PasswordSaltCust1, PasswordHashCust2, PasswordSaltCust2, PasswordHashCust3, PasswordSaltCust3;

            CreatePasswordHash(password, out PasswordHashCust1, out PasswordSaltCust1);
            CreatePasswordHash(password, out PasswordHashCust2, out PasswordSaltCust2);
            CreatePasswordHash(password, out PasswordHashCust3, out PasswordSaltCust3);
            ctx.Customers.Add(new Customer()
            {
                FirstName = "Meg",
                LastName = "Bramhill",
                Email = "megb@outlook.com",
                PasswordHash = PasswordHashCust1,
                PasswordSalt = PasswordSaltCust1,
                IsAdmin = false
            });
            ctx.Customers.Add(new Customer()
            {
                FirstName = "Jimmy",
                LastName = "Venton",
                Email = "jimmyv@outlook.com",
                PasswordHash = PasswordHashCust2,
                PasswordSalt = PasswordSaltCust2,
                IsAdmin = false
            });
            ctx.Customers.Add(new Customer()
            {
                FirstName = "Carl",
                LastName = "Grimes",
                Email = "carlg@outlook.com",
                PasswordHash = PasswordHashCust3,
                PasswordSalt = PasswordSaltCust3,
                IsAdmin = false
            });
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
