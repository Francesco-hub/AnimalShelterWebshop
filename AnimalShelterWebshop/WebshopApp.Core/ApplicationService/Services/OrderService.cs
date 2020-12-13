using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepo;
        readonly ICustomerRepository _customerRepo;

        public OrderService (IOrderRepository orderRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }
        public Order CreateOrder(Order ord)
        {
            if(ord.CustomerId == 0 || ord.CustomerId < 0)
            {
                throw new InvalidDataException("You need a Customer to create an order");
            }
            if (_customerRepo.ReadCustomerByIDIncludingOrders(ord.CustomerId) == null)
            {
                throw new InvalidDataException("Customer not found");
            }
            if (ord.OrderDate == null)
            {
                throw new InvalidDataException("Order needs an order date");
            }
            return _orderRepo.Create(ord);
        }

        public Order DeleteOrderByID(int id)
        {
            return _orderRepo.Delete(id);
        }

        public List<Order> FindOrderByCustomerID(int id)
        {
            return _orderRepo.ReadOrderByCustomerID(id).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepo.ReadAllOrders().ToList();
        }

        public List<Order> GetFilteredOrders(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("Current page and items per must be 0 or more");
            }
            if ((filter.CurrentPage - 1 * filter.ItemsPerPage) >= _orderRepo.Count())
            {
                throw new InvalidDataException("index out of bounds current page is too high");
            }
            return _orderRepo.ReadAllOrders().ToList();
        }

        public Order UpdateOrder(Order OrdUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
