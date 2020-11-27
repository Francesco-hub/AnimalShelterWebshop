using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer Create(Customer cust)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Customer> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Customer ReadCustomerByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer ReadCustomerByIDIncludingOrders(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Update(Customer custUpdate)
        {
            throw new NotImplementedException();
        }
        public Customer DeleteByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
