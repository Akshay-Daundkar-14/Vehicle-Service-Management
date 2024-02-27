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

            var vehicleDomain = await _service.GetAllVehicleAsync();
            if (vehicleDomain == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Vehicles\" retrieved successfully");
            return Ok(vehicleDomain);

        }

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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId || vehicle == null)
            {
                return BadRequest();
            }


            _logger.LogInformation($"\"Vehicle\" with Id --> {id} get updated successfully.");
            await _service.UpdateVehicleAsync(vehicle);


            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {

            if (vehicle == null)
            {
                return Problem("Entity set 'AppDbContext.Vehicles'  is null.");
            }
            _logger.LogInformation($"\"Vehicle\" created successfully with id -> {vehicle.VehicleId}");
            await _service.CreateVehicleAsync(vehicle);

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);

        }


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
