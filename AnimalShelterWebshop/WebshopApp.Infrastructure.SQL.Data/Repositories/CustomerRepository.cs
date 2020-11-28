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
            return _ctx.Customers;
        }

        public Customer ReadCustomerByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer ReadCustomerByIDIncludingOrders(int id)
        {
            return _ctx.Customers
                .Include(c => c.OrderList)
                .FirstOrDefault(c => c.ID == id);
        }

        public Customer Update(Customer custUpdate)
        {
            throw new NotImplementedException();
        }
        public Customer DeleteByID(int id)
        {
            var custRemoved = _ctx.Remove<Customer>(new Customer { ID = id }).Entity;
            _ctx.SaveChanges();
            return custRemoved;
        }
    }
}
