using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Core.ApplicationService;
using WebshopApp.Core.Entity;
using WebshopRestAPI.DTO;

namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]

    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers (read all)
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get()
        {
            List<Customer> DBcustLst =  _customerService.GetAllCustomers();
            List<CustomerDTO> custLst = new List<CustomerDTO>(); 
            foreach(Customer cust in DBcustLst)
            {
                List<Order> custOrdLst = cust.Orders;
                CustomerDTO custDTO =  new CustomerDTO
                {
                    Id = cust.Id,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Email= cust.Email,
                    Password = cust.Password,
                    Orders = new List<OrderDTO>() {}
                };
                foreach(Order ord in custOrdLst)
                {
                    List<Product> ordProdLst = ord.Products;
                    OrderDTO ordDTO = new OrderDTO
                    {
                        Id = ord.Id,
                        CustomerId = ord.CustomerId,
                        OrderDate = ord.OrderDate,
                        DeliveryDate = ord.DeliveryDate,
                        DeliveryAddress = ord.DeliveryAddress,
                        TotalPrice = ord.TotalPrice,
                        Products = new List<ProductDTO>() {}

                    };
                    foreach (Product prod in ordProdLst)
                    {
                        ordDTO.Products.Add(substituteProduct(prod));
                    }
                    custDTO.Orders.Add(ordDTO);
                }
                custLst.Add(custDTO);
            }
            return custLst;
        }

        // GET: api/customers/5 (read by id)
        //[Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("ID must be bigger than 0");
            }
            Customer dbCust = _customerService.FindCustomerByIDIncludingOrders(id);
            List<Order> custOrdLst = dbCust.Orders;
            CustomerDTO custDTO = new CustomerDTO
            {
                Id = dbCust.Id,
                FirstName = dbCust.FirstName,
                LastName = dbCust.LastName,
                Email = dbCust.Email,
                Password = dbCust.Password,
                Orders = new List<OrderDTO>() { }
            };
            foreach (Order ord in custOrdLst)
            {
                List<Product> ordProdLst = ord.Products;
                OrderDTO ordDTO = new OrderDTO
                {
                    Id = ord.Id,
                    CustomerId = ord.CustomerId,
                    OrderDate = ord.OrderDate,
                    DeliveryDate = ord.DeliveryDate,
                    DeliveryAddress = ord.DeliveryAddress,
                    TotalPrice = ord.TotalPrice,
                    Products = new List<ProductDTO>() { }

                };
                foreach (Product prod in ordProdLst)
                {
                    ordDTO.Products.Add(substituteProduct(prod));
                }
                custDTO.Orders.Add(ordDTO);
            }
            return custDTO;

        }

        // POST: api/customer (create json)
        //[Authorize(Roles = "Administrator")]
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
       // [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
        {
            if (id < 1 || id != customer.Id)
            {
                return BadRequest("Parameter ID and CustomerID must be the same");
            }
            return Ok(_customerService.UpdateCustomer(customer));
        }

        //DELETE api/customer/5
        //[Authorize(Roles = "Administrator")]
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
