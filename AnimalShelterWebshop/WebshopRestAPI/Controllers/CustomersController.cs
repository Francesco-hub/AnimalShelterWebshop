using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.Entity;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers (read all)
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _customerService.GetAllCustomers();
        }

        // GET: api/customers/5 (read by id)
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be bigger than 0");
            }
            return _customerService.FindCustomerByIDIncludingOrders(id);
        }

        // POST: api/customer (create json)
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                BadRequest("First name is required for creating a customer");
            }
            if (string.IsNullOrEmpty(customer.LastName))
            {
                BadRequest("Last name is required for creating a customer");
            }
            return Ok(_customerService.CreateCustomer(customer));
        }

        //PUT api/customers/5 (update)
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.ID)
            {
                return BadRequest("Parameter ID and CustomerID must be the same");
            }
            return Ok(_customerService.UpdateCustomer(customer));
        }

        //DELETE api/customer/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _customerService.DeleteCustomerByID(id);
            if (customer == null)
            {
                return StatusCode(404, $"Did not find Customer with ID {id}");
            }
            return Ok($"Customer with ID {id} has been deleted");
        }
    }
}
