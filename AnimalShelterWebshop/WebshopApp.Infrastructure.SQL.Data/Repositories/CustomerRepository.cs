using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly WebshopAppContext _ctx;

        public CustomerRepository(WebshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public Customer Create(Customer cust)
        {
            var newCustomer = _ctx.Customers.Add(cust).Entity;
            _ctx.SaveChanges();
            return newCustomer;
        }

        public IEnumerable<Customer> ReadAllCustomers()
        {
            return _ctx.Customers.Include(c => c.Orders).ThenInclude(o => o.Products)
                .Select(c =>
                    new Customer()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        PasswordHash = c.PasswordHash,
                        PasswordSalt = c.PasswordSalt,
                        IsAdmin = c.IsAdmin,
                        Orders = c.Orders.Select(o => new Order()
                        {
                            Id = o.Id,
                            Customer = o.Customer,
                            CustomerId = o.CustomerId,
                            DeliveryAddress = o.DeliveryAddress,
                            OrderDate = o.OrderDate,
                            DeliveryDate = o.DeliveryDate,
                            OrderProducts = o.OrderProducts,
                            TotalPrice = o.TotalPrice,
                            Products = o.Products.Select(p => new Product()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                TypeName = p.TypeName,
                                ImageUrl = p.ImageUrl,
                                OrderProducts = p.OrderProducts,
                                Orders = p.Orders,
                                Price = p.Price
                            }).ToList()
                        }).ToList()

                    }

                );

        }

        public Customer ReadCustomerByIDIncludingOrders(int id)
        {
            List<Customer> allCustLst =  _ctx.Customers.Include(c => c.Orders).ThenInclude(o => o.Products)
                .Select(c =>
                    new Customer()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        PasswordHash = c.PasswordHash,
                        PasswordSalt = c.PasswordSalt,
                        IsAdmin = c.IsAdmin,
                        Orders = c.Orders.Select(o => new Order()
                        {
                            Id = o.Id,
                            Customer = o.Customer,
                            CustomerId = o.CustomerId,
                            DeliveryAddress = o.DeliveryAddress,
                            OrderDate = o.OrderDate,
                            DeliveryDate = o.DeliveryDate,
                            OrderProducts = o.OrderProducts,
                            TotalPrice = o.TotalPrice,
                            Products = o.Products.Select(p => new Product()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                TypeName = p.TypeName,
                                ImageUrl = p.ImageUrl,
                                OrderProducts = p.OrderProducts,
                                Orders = p.Orders,
                                Price = p.Price
                            }).ToList()
                        }).ToList()

                    }

                ).ToList();
            return allCustLst.FirstOrDefault(c => c.Id == id);
        }

        public Customer Update(Customer custUpdate)
        {
            throw new NotImplementedException();
        }
        public Customer DeleteByID(int id)
        {
            var custRemoved = _ctx.Remove(new Customer { Id = id });
            _ctx.SaveChanges();
            return custRemoved.Entity;
        }
    }
}
