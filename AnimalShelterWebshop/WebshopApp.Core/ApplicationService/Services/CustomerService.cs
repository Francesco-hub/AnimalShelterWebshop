﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository _customerRepo;
        readonly IOrderRepository _orderRepo;
        private readonly ICustomerValidator _customerValidator;

        public CustomerService(ICustomerValidator customerValidator,
                                ICustomerRepository customerRepository)
        {
            if (customerValidator == null) throw new NullReferenceException("Validator cannot be null");
            if (customerRepository == null) throw new NullReferenceException("CustomerRepository cannot be null");

            _customerRepo = customerRepository;
            _customerValidator = customerValidator;
        }


        public CustomerService (ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepo = customerRepository;
            _orderRepo = orderRepository;
        }

        public Customer NewCustomer(string firstName, string lastName, string address, bool isAdmin, string email, byte[] passwordHash, byte[]passwordSalt, List<Order> orderlist)
        {
            var cust = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                IsAdmin = isAdmin,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Orders = orderlist
            };
            return cust;
        }

        public Customer CreateCustomer(Customer cust)
        {
            return _customerRepo.Create(cust);
        }      

        public Customer FindCustomerByIDIncludingOrders(int id)
        {
            var cust = _customerRepo.ReadCustomerByIDIncludingOrders(id);
            return cust;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.ReadAllCustomers().ToList();
        }

        public Customer UpdateCustomer(Customer custUpdate)
        {
            throw new NotImplementedException();
        }

        public Customer DeleteCustomerByID(int id)
        {
            return _customerRepo.DeleteByID(id);
        }
    }
}
