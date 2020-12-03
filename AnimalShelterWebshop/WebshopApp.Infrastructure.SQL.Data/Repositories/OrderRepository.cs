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
        public int Count()
        {
            return _ctx.Orders.Count();
        }
        public Order Create(Order ord)
        {
            _ctx.Attach(ord).State = EntityState.Added;
            _ctx.SaveChanges();
            return ord;
        }

        public IEnumerable<Order> ReadAllOrders(Filter filter)
        {
            if (filter == null)
            {
                return _ctx.Orders;
            }
            return _ctx.Orders
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage);
        }

        public Order ReadOrderByID(int id)
        {
            return _ctx.Orders.Include(o => o.CustomerID)
                .FirstOrDefault(o => o.ID == id);
        }

        public Order Update(Order ordUpdate)
        {
            throw new NotImplementedException();
        }
        public Order Delete(int id)
        {
            var removed = _ctx.Remove(new Order { ID = id }).Entity;
            _ctx.SaveChanges();
            return removed;
        }
    }
}
