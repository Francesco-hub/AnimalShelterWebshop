using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopRestAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TypeName { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

    }
}
