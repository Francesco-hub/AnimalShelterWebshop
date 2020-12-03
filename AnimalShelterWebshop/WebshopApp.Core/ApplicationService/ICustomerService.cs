using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService
{
    public interface ICustomerService
    {
        //Create a new customer
        Customer NewCustomer(string firstName, string lastName, string address, string email, string password, List<Order> orderlist);

        //POST (CREATE)
        Customer CreateCustomer(Customer cust);

        //GET (READ)
        Customer FindCustomerByIDIncludingOrders(int id);
        List<Customer> GetAllCustomers();

        //PUT (UPDATE)
        Customer UpdateCustomer(Customer custUpdate);

        //DELETE
        Customer DeleteCustomerByID(int id);
    }
}
