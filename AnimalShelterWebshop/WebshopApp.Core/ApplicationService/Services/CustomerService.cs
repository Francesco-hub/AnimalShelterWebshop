using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService.Services
{
    public class CustomerService : ICustomerService
    {
        /*readonly ICustomerRepository _customerRepo;
        readonly IOrderRepository _orderRepo;*/

        public ICustomerService NewCustomer(string FirstName, string LastName, string Address)
        {
            throw new NotImplementedException();
        }

        public Customer CreateCustomer(Customer cust)
        {
            throw new NotImplementedException();
        }      

        public Customer FindCustomerByIDIncludingOrders(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer UpdateCustomer(Customer custUpdate)
        {
            throw new NotImplementedException();
        }

        public Customer DeleteCustomerByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
