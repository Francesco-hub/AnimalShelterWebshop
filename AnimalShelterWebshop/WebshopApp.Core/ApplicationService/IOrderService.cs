using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService
{
    public interface IOrderService
    {
        /*//Create a new order
        Order New();*/

        //POST (CREATE)
        Order CreateOrder(Order ord);

        //GET (READ)
        Order FindOrderByID(int id);
        List<Order> GetAllOrders();
        List<Order> GetFilteredOrders(Filter filter);

        //PUT (UPDATE)
        Order UpdateOrder(Order OrdUpdate);

        //DELETE
        Order DeleteOrderByID(int id);
    }
}
