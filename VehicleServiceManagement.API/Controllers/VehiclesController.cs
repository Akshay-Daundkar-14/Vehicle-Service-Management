using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _service;
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(IVehicleRepository service, ILogger<VehiclesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            try
            {
                var vehicleDomain = await _service.GetAllVehicleAsync();
                if (vehicleDomain == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"\"Vehicles\" retrieved successfully");
                return Ok(vehicleDomain);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Vehicles\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle([FromRoute] int id)
        {
            try
            {
                if (_service.GetAllVehicleAsync == null || id <= 0)
                {
                    return NotFound();
                }
                var vehicle = await _service.GetVehicleAsync(id);

                if (vehicle == null)
                {
                    return NotFound();
                }
                _logger.LogInformation($"\"Vehicle\" retrieved successfully with id -> {id}");
                return vehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving \"Vehicle\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute]int id, [FromBody]Vehicle vehicle)
        {
            if (id != vehicle.VehicleId || vehicle == null)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation($"\"Vehicle\" with Id --> {id} get updated successfully.");
                await  _service.UpdateVehicleAsync(vehicle);
            }
            catch (Exception ex)
            {
                if (_service.GetVehicleAsync(id) ==null)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error updating \"vehicle\" with id -> {id}: {ex.Message}");
                    return StatusCode(500, "Internal server server");
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                if (_service.GetAllVehicleAsync == null || vehicle == null)
                {
                    return Problem("Entity set 'AppDbContext.Vehicles'  is null.");
                }
                _logger.LogInformation($"\"Vehicle\" created successfully with id -> {vehicle.VehicleId}");
                await _service.CreateVehicleAsync(vehicle);

                return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Creating \"Vehicle\": {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute]int id)
        {
            try
            {
                if (_service.GetAllVehicleAsync == null || id <= 0)
                {
                    return NotFound();
                }
                var vehicle = await _service.GetVehicleAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                await _service.DeleteVehicleAsync(vehicle);
                _logger.LogInformation($"\"Vehicle\" deleted successfully with id -> {vehicle.VehicleId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting \"Vehicle\" with id -> {id}: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }
        }

       
    }
}
