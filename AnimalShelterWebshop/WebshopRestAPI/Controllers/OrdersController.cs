using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.Entity;
using WebshopRestAPI.DTO;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //GET: api/orders (read all)
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get()
        {
            try
            {
                List<Order> dbOrdLst = _orderService.GetAllOrders();
                List<OrderDTO> ordLst = new List<OrderDTO>();
                foreach(Order ord in dbOrdLst)
                {
                    List<Product> ordProdLst = ord.Products;
                    OrderDTO ordtDTO = new OrderDTO
                    {
                        Id = ord.Id,
                        CustomerId = ord.CustomerId,
                        OrderDate = ord.OrderDate,
                        DeliveryDate = ord.DeliveryDate,
                        DeliveryAddress = ord.DeliveryAddress,
                        Products = new List<ProductDTO> { },
                        TotalPrice = ord.TotalPrice

                    };
                    foreach (Product prod in ordProdLst)
                    {
                        ordtDTO.Products.Add(substituteProductToProductDto(prod));
                    }
                    ordLst.Add(ordtDTO);
                }
                return ordLst;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[HttpGet]
        //public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        //{
        //    try
        //    {
        //        return Ok(_orderService.GetFilteredOrders(filter));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //GET: api/orders/5
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> Get(int id)
        {
            try
            {
                Order dbOrd = _orderService.FindOrderByID(id);
                    List<Product> ordProdLst = dbOrd.Products;
                    OrderDTO ordDTO = new OrderDTO
                    {
                        Id = dbOrd.Id,
                        CustomerId = dbOrd.CustomerId,
                        OrderDate = dbOrd.OrderDate,
                        DeliveryDate = dbOrd.DeliveryDate,
                        DeliveryAddress = dbOrd.DeliveryAddress,
                        Products = new List<ProductDTO> { },
                        TotalPrice = dbOrd.TotalPrice

                    };
                    foreach (Product prod in ordProdLst)
                    {
                        ordDTO.Products.Add(substituteProductToProductDto(prod));
                    }
                return ordDTO;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public ActionResult<OrderDTO> Post([FromBody] OrderDTO orderDto)
        {
            try
            {
                Order newOrder = new Order 
                { 
                Id = orderDto.Id,
                OrderDate = orderDto.OrderDate,
                DeliveryDate = orderDto.DeliveryDate,
                CustomerId = orderDto.CustomerId,
                DeliveryAddress = orderDto.DeliveryAddress,
                Products = new List<Product> { },
                TotalPrice = 0
                };
                foreach (ProductDTO prodDto in orderDto.Products)
                {
                    newOrder.Products.Add(substituteProductDtoToProduct(prodDto));
                    newOrder.TotalPrice += prodDto.Price;
                }
                return Ok(_orderService.CreateOrder(newOrder));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT: api/orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
            {
                return BadRequest("Parameter ID and OrderID must be the same");
            }
            return Ok(_orderService.UpdateOrder(order));
        }

        //DELETE: api/orders/5
        /*[HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            var order = _orderService.DeleteOrderByID(id);
            if (order == null)
            {
                return StatusCode(404, $"Did not find Order with ID {id}");
            }
            return Ok($"Order with ID {id} has been deleted");
        }*/
        public ProductDTO substituteProductToProductDto(Product product)
        {
            ProductDTO prodDTO = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                TypeName = product.TypeName,
                Price = product.Price
            };
            return prodDTO;
        }
        public Product substituteProductDtoToProduct(ProductDTO productDto)
        {
            Product prod = new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                TypeName = productDto.TypeName,
                Price = productDto.Price,
                Orders = new List<Order> { },
                OrderProducts = new List<OrderProduct> { }
            };
            return prod;
        }
    }
}
