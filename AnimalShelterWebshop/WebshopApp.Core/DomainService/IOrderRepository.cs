using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.DomainService
{
    public interface IOrderRepository
    {
        //Create data
        Order Create(Order ord);

        //Read data
        IEnumerable<Order> ReadOrderByCustomerID(int id);
        IEnumerable<Order> ReadAllOrders();
        int Count();

        //Update data
        Order Update(Order ordUpdate);

        //Delete data
        Order Delete(int id);    }
}
