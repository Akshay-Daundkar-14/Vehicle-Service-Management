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
        //[Authorize(Roles = "Service Advisor")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {

            var vehicleDomain = await _service.GetAllVehicleAsync();
            if (vehicleDomain == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Vehicles\" retrieved successfully");
            return Ok(vehicleDomain);

        }

        
        //[Authorize(Roles = "Service Advisor")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle([FromRoute] int id)
        {

            if (id <= 0)
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

        

       // [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId || vehicle == null)
            {
                return BadRequest();
            }

            vehicle.IsDeleted = false;
            vehicle.UpdatedDate = DateTime.Now;            


            _logger.LogInformation($"\"Vehicle\" with Id --> {id} get updated successfully.");
            await _service.UpdateVehicleAsync(vehicle);


            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {

            if (vehicle == null)
            {
                return Problem("Entity set 'AppDbContext.Vehicles'  is null.");
            }
            vehicle.IsDeleted = false;
            vehicle.CreatedDate = DateTime.Now;
            vehicle.VehicleStatus = "Pending";
            await _service.CreateVehicleAsync(vehicle);
            _logger.LogInformation($"\"Vehicle\" created successfully with id -> {vehicle.VehicleId}");

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {

            if (id <= 0)
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


    }
}
