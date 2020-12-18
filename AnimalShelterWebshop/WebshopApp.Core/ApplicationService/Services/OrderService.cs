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
        private readonly IOrderValidator _orderValidator;


        public OrderService(IOrderValidator orderValidator,
                                IOrderRepository orderRepo)
        {
            if (orderValidator == null) throw new NullReferenceException("Validator cannot be null");
            if (orderRepo == null) throw new NullReferenceException("OrderRepository cannot be null");

            _orderRepo = orderRepo;
            _orderValidator = orderValidator;
        }


        public OrderService (IOrderRepository orderRepo, ICustomerRepository customerRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }


        public Order CreateOrder(Order ord)
        {
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


        public Order UpdateOrder(Order OrdUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
