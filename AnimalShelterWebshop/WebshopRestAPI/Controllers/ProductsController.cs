using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                    Price = prod.Price,
                    ImageUrl = prod.ImageUrl
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
                    Price = prod.Price,
                    ImageUrl = prod.ImageUrl
                });
            }
            return prodLst;
        }

        // POST: api/customer (create json)
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
            public ActionResult<Product> Post([FromBody] ProductDTO product)
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    BadRequest("Name is required for creating a Product");
                }
                if (string.IsNullOrEmpty(product.TypeName))
                {
                    BadRequest("Type is required for creating a Product");
                }
            Product newProduct = new Product {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                TypeName = product.TypeName,
                ImageUrl = product.ImageUrl,
                Orders = new List<Order>(),
                OrderProducts = new List<OrderProduct>()
            };
                return Ok(_productService.CreateProduct(newProduct));
            }

            //PUT api/customers/5 (update)
            // [Authorize(Roles = "Administrator")]
            [HttpPut("{id}")]
            public ActionResult<ProductDTO> Put(int id, [FromBody] ProductDTO productDto)
            {
                if (id < 1 || id != productDto.Id)
                {
                    return BadRequest("Parameter ID and ProductID must be the same");
                }
            Product product = new Product()
            {
            Id = id,
            Name = productDto.Name,
            TypeName = productDto.TypeName,
            Price = productDto.Price,
            ImageUrl = productDto.ImageUrl
            };
            Product updatedProd = _productService.UpdateProduct(product);
            ProductDTO updatedProdDto = new ProductDTO 
            {
                Id = updatedProd.Id,
                Name = updatedProd.Name,
                TypeName = updatedProd.TypeName,
                Price = updatedProd.Price,
                ImageUrl = updatedProd.ImageUrl
            };
            return Ok(updatedProdDto);
            }

            //DELETE api/customer/5
            //[Authorize(Roles = "Administrator")]
            [HttpDelete("{id}")]
            public ActionResult<Product> Delete(int id)
            {
            try
            {
                _productService.DeleteProduct(id);
                return Ok();
            }
            catch(Exception E)
            {
                return StatusCode(404);
            }
               // var product = _productService.DeleteProduct(id);
                //if (product == null)
                //{
                   // return StatusCode(404, $"Did not find Product with ID {id}");
                //}
                //return Ok($"Product with ID {id} has been deleted");
            }
        
    }
}
