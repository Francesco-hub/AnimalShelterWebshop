using System;
using System.Collections.Generic;
using System.Text;

namespace WebshopApp.Core.Entity
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public List<Order> OrderList { get; set; }
    }
}
