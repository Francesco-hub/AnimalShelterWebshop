using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TypeName { get; set; }

        public double Price { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public List<Order> Orders { get; set; }

        public string ImageUrl { get; set; }
    }
}
