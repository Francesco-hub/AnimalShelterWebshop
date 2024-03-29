﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public List<Product> Products { get; set; }

        public double TotalPrice { get; set; }

        
    }
}
