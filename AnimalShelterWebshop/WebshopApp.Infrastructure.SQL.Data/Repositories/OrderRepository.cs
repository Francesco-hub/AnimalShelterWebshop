using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        readonly WebshopAppContext _ctx;

        public Order Create(Order ord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAllOrders(Filter filter = null)
        {
            throw new NotImplementedException();
        }

        public Order ReadOrderByID(int id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order ordUpdate)
        {
            throw new NotImplementedException();
        }
        public Order Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
