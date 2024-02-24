using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _service;

        public CustomersController(ICustomerRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customer = await _service.GetAllCustomerAsync();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer([FromRoute] int id)
        {
            if (_service.GetAllCustomerAsync == null || id <= 0)
            {
                return NotFound();
            }
            var customer = await _service.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute]int id, [FromBody]Customer customer)
        {
            if (id != customer.CustomerId || customer == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateCustomerAsync(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetCustomerAsync(id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            if (_service.GetAllCustomerAsync == null || customer == null)
            {
                return Problem("Entity set 'AppDbContext.Customers'  is null.");
            }

           await _service.CreateCustomerAsync(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute]int id)
        {
            if (_service.GetAllCustomerAsync == null || id <=0)
            {
                return NotFound();
            }
            var customer = await _service.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

           await _service.DeleteCustomerAsync(customer);

            return NoContent();
        }

       
    }
}
