using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.Entity;
using WebshopRestAPI.DTO;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : Controller
    {
            private readonly IProductService _productService;

            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }

            // GET: api/products (read all)
            [HttpGet]
            public ActionResult<IEnumerable<ProductDTO>> Get()
            {
            List<Product> dbProdLst = _productService.GetAllProducts();
            List<ProductDTO> prodLst = new List<ProductDTO>();
            foreach (Product prod in dbProdLst)
            {
                prodLst.Add(new ProductDTO
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    TypeName = prod.TypeName,
                    Price = prod.Price
                });
            }
            return prodLst;
            }

            // GET: api/products/5 (read by id)
            //[Authorize(Roles = "Administrator")]
            //[HttpGet("{id}")]
            //public ActionResult<ProductDTO> Get(int id)
            //{
            //    if (id < 1)
            //    {
            //        return BadRequest("ID must be bigger than 0");
            //    }
            //    Product dbProd = _productService.FindProductByID(id);
            //    return new ProductDTO{
            //        Id = dbProd.Id,
            //        Name = dbProd.Name,
            //        TypeName = dbProd.TypeName,
            //        Price = dbProd.Price
            //    };
            //}

        [HttpGet("{TypeName}")]
        public ActionResult<IEnumerable <ProductDTO>> Get (string typeName)
        {
            List<Product> dbProdLst = _productService.GetAllByType(typeName);
            List<ProductDTO> prodLst = new List<ProductDTO>();
            foreach(Product prod in dbProdLst)
            {
                prodLst.Add(new ProductDTO
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    TypeName = prod.TypeName,
                    Price = prod.Price
                });
            }
            return prodLst;
        }

        // POST: api/customer (create json)
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
            public ActionResult<Product> Post([FromBody] Product product)
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    BadRequest("Name is required for creating a Product");
                }
                if (string.IsNullOrEmpty(product.TypeName))
                {
                    BadRequest("Type is required for creating a Product");
                }
                return Ok(_productService.CreateProduct(product));
            }

            //PUT api/customers/5 (update)
            // [Authorize(Roles = "Administrator")]
            [HttpPut("{id}")]
            public ActionResult<Product> Put(int id, [FromBody] Product product)
            {
                if (id < 1 || id != product.Id)
                {
                    return BadRequest("Parameter ID and ProductID must be the same");
                }
                return Ok(_productService.UpdateProduct(product));
            }

            //DELETE api/customer/5
            //[Authorize(Roles = "Administrator")]
            [HttpDelete("{id}")]
            public ActionResult<Product> Delete(int id)
            {
                var product = _productService.DeleteProduct(id);
                if (product == null)
                {
                    return StatusCode(404, $"Did not find Product with ID {id}");
                }
                return Ok($"Product with ID {id} has been deleted");
            }
        
    }
}
