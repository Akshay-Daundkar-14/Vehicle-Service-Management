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
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _service;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerRepository service, ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {

            //------------------------ Logging Error Message --------------

            //_logger.LogTrace("Log message from trace method");
            //_logger.LogDebug("Log message from debug method");
            //_logger.LogInformation("Log message from information method");
            //_logger.LogWarning("Log message from warning method");
            //_logger.LogError("Log message from error method");
            //_logger.LogCritical("Log message from critical method");

            //------------------------ Logging Error Message --------------


            try
            {
                var customer = await _service.GetAllCustomerAsync();
                if (customer == null)
                {
                    return NotFound();
                }

                _logger.LogInformation($"\"Customers\" retrieved successfully");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Customers\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer([FromRoute] int id)
        {
            try
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

                _logger.LogInformation($"\"Customer\" retrieved successfully with id -> {id}");

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Customer\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }

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
               _logger.LogInformation($"\"Customer\" with id -> {id} updated successfully.");
            }
            catch (Exception ex)
            {
                if (_service.GetCustomerAsync(id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error updating \"Customer\" with id -> {id}: {ex.Message}");
                    return StatusCode(500, "Internal server server");
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            try
            {
                if (_service.GetAllCustomerAsync == null || customer == null)
                {
                    return Problem("Entity set 'AppDbContext.Customers'  is null.");
                }

                await _service.CreateCustomerAsync(customer);
                _logger.LogInformation($"\"Customer\" created successfully with id -> {customer.CustomerId}");
                return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Creating \"Customer\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute]int id)
        {
            try
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

                await _service.DeleteCustomerAsync(customer);
                _logger.LogInformation($"\"Customer\" deleted successfully with id -> {customer.CustomerId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting \"Customer\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
    }
}
