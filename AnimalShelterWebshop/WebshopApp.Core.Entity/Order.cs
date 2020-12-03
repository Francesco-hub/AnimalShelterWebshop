using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Core.Entity
{
    public class Order
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public List<Product> ProductList { get; set; }

        public double TotalPrice { get; set; }

        
    }
}
