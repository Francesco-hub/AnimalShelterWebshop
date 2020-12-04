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
            IEnumerable<Customer> allCustomers = _ctx.Customers;
            IEnumerable<Order> AllOrders = _ctx.Orders;
            foreach (Order ord in AllOrders)
            {
                foreach (Customer cust in allCustomers)
                {
                    if (ord.CustomerID == cust.ID)
                    {
                        cust.OrderList.Add(ord);
                    }
                }
            }
            return allCustomers;

        }

        public Customer ReadCustomerByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer ReadCustomerByIDIncludingOrders(int id)
        {
            Customer foundCustomer =  _ctx.Customers.FirstOrDefault(c => c.ID == id);
            IEnumerable<Order> AllOrders = _ctx.Orders;
            int a = AllOrders.Count();
            foreach(Order ord in AllOrders)
            {
                if(foundCustomer.ID == ord.CustomerID)
                {
                    foundCustomer.OrderList.Add(ord);
                }
            }
            return foundCustomer;
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
