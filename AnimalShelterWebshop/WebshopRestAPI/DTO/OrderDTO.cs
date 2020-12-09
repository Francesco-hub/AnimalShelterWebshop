using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopRestAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryAddress { get; set; }

        public List<ProductDTO> Products { get; set; }

        public double TotalPrice { get; set; }
    }
}
