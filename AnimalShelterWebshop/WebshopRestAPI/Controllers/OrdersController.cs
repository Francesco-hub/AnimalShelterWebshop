using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.Entity;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //GET: api/orders (read all)
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_orderService.GetFilteredOrders(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //GET: api/orders/5
        [HttpGet("{id")]
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
            if (id < 1 || id != order.ID)
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
    }
}
