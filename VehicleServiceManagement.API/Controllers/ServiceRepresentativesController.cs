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
    public class ServiceRepresentativesController : ControllerBase
    {
        private readonly IServiceRepresentativeRepository _service;
        private readonly ILogger<ServiceRepresentativesController> _logger;

        public ServiceRepresentativesController(IServiceRepresentativeRepository service, ILogger<ServiceRepresentativesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRepresentative>>> GetServiceRepresentatives()
        {
            try
            {
                var serviceRepresentativeDomain = await _service.GetAllServiceRepresentativeAsync();
                if (serviceRepresentativeDomain == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"\"Service Representative\" retrieved successfully");
                return Ok(serviceRepresentativeDomain);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Service Representative\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRepresentative>> GetServiceRepresentative([FromRoute] int id)
        {
            try
            {
                if (_service.GetAllServiceRepresentativeAsync == null || id <= 0)
                {
                    return NotFound();
                }
                var serviceRepresentative = await _service.GetServiceRepresentativeAsync(id);

                if (serviceRepresentative == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"\"Service Representative\" retrieved successfully with id -> {id}");
                return serviceRepresentative;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Service Representative\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRepresentative([FromRoute]int id, [FromBody]ServiceRepresentative serviceRepresentative)
        {
            if (id != serviceRepresentative.RepresentativeID || serviceRepresentative == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateServiceRepresentativeAsync(serviceRepresentative);
                _logger.LogInformation($"\"Service Representative\" with id -> {id} updated successfully.");
            }
            catch (Exception ex)
            {
                if (_service.GetServiceRepresentativeAsync(id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error updating \"Service Representative\" with id -> {id}: {ex.Message}");
                    return StatusCode(500, "Internal server server");
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<ServiceRepresentative>> PostServiceRepresentative([FromBody] ServiceRepresentative serviceRepresentative)
        {
            try
            {
                if (_service.GetAllServiceRepresentativeAsync == null || serviceRepresentative == null)
                {
                    return Problem("Entity set 'AppDbContext.ServiceRepresentatives'  is null.");
                }

                await _service.CreateServiceRepresentativeAsync(serviceRepresentative);
                _logger.LogInformation($"\"Service Representative\" created successfully with id -> {serviceRepresentative.RepresentativeID}");
                return CreatedAtAction("GetServiceRepresentative", new { id = serviceRepresentative.RepresentativeID }, serviceRepresentative);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Creating \"Service Representative\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRepresentative([FromRoute]int id)
        {
            try
            {
                if (_service.GetAllServiceRepresentativeAsync == null || id <= 0)
                {
                    return NotFound();
                }
                var serviceRepresentative = await _service.GetServiceRepresentativeAsync(id);
                if (serviceRepresentative == null)
                {
                    return NotFound();
                }

                await _service.DeleteServiceRepresentativeAsync(serviceRepresentative);
                _logger.LogInformation($"\"Service Representative\" deleted successfully with id -> {serviceRepresentative.RepresentativeID}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting \"Service Representative\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
    }
}
