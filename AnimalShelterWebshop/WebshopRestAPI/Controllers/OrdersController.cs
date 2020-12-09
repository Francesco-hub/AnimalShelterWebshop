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
                        ordtDTO.Products.Add(substituteProduct(prod));
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
                        ordDTO.Products.Add(substituteProduct(prod));
                    }
                return ordDTO;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
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
        public ProductDTO substituteProduct(Product product)
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
    }
}
