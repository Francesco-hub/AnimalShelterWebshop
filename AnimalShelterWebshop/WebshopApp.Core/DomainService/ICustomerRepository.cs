using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.DomainService
{
    public interface ICustomerRepository
    {
        //Create data
        Customer Create(Customer cust);

        //Read data
        Customer ReadCustomerByID(int id);
        IEnumerable<Customer> ReadAllCustomers();
        Customer ReadCustomerByIDIncludingOrders(int id);

        //Update data
        Customer Update(Customer custUpdate);

        //Delete data
        Customer DeleteByID(int id);

    }
}
