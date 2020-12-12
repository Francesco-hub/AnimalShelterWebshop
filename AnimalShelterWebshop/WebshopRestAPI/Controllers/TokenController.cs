using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;
using WebshopRestAPI.Helpers;
using WebshopRestAPI.Models;


namespace WebshopRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private ICustomerRepository CustomerRepository;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(ICustomerRepository custRepo, IAuthenticationHelper authHelper)
        {
            CustomerRepository = custRepo;
            authenticationHelper = authHelper;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var customer = CustomerRepository.ReadAllCustomers().FirstOrDefault(u => u.Email == model.Email);

            if (customer == null)
                return Unauthorized();

            if (!authenticationHelper.VerifyPasswordHash(model.Password, customer.PasswordHash, customer.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                email = customer.Email,
                token = authenticationHelper.GenerateToken(customer)
            });
        }

    }
}
