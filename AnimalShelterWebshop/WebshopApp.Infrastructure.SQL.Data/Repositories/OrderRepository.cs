using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        readonly WebshopAppContext _ctx;

        public OrderRepository (WebshopAppContext ctx)
        {
            _ctx = ctx;
        }
        public Order Create(Order ord)
        {
            _ctx.Attach(ord).State = EntityState.Added;
            _ctx.SaveChanges();
            return ord;
        }

        public IEnumerable<Order> ReadAllOrders()
        {
            return _ctx.Orders.Include(p => p.Products).IgnoreQueryFilters().
            Select(o => new Order()
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
                    OrderProducts = p.OrderProducts,
                    Orders = p.Orders,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToList()
            }).ToList();
        }

        public IEnumerable<Order> ReadOrderByCustomerID(int id)
        {
            try
            {
                List<Order> dbOrdLst = new List<Order>();
                dbOrdLst = _ctx.Orders.Include(p => p.Products).IgnoreQueryFilters().
                Select(o => new Order()
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
                        OrderProducts = p.OrderProducts,
                        Orders = p.Orders,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    }).ToList()
                }).ToList();
                return dbOrdLst.Where(o => o.CustomerId == id);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public Order Update(Order ordUpdate)
        {
            throw new NotImplementedException();
        }
        public Order Delete(int id)
        {
            var removed = _ctx.Remove(new Order { Id = id }).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}
