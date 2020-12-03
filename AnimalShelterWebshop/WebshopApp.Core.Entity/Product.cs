using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Core.Entity
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string TypeName { get; set; }

        public double Price { get; set; }
    }
}
