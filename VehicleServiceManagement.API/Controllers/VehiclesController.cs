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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _service;

        public VehiclesController(IVehicleRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicleDomain = await _service.GetAllVehicleAsync();
            if (vehicleDomain == null)
            {
                return NotFound();
            }

            //var VehicleDto = new List<VehicleDTO>();

            //foreach (var VehicleDomains in VehicleDomain)
            //{
            //    VehicleDto.Add(new VehicleDTO()
            //    {
            //        VehicleId = VehicleDomains.VehicleId,
            //        FirstName = VehicleDomains.FirstName,
            //        LastName = VehicleDomains.LastName,
            //        Email = VehicleDomains.Email,
            //        Password = VehicleDomains.Password,
            //        Address = VehicleDomains.Address,
            //        Mobile = VehicleDomains.Mobile,
            //        Image = VehicleDomains.Image,
            //        IsDeleted = VehicleDomains.IsDeleted,
            //        CreatedDate = VehicleDomains.CreatedDate,
            //        UpdatedDate = VehicleDomains.UpdatedDate,
            //    });
            //}
            return Ok(vehicleDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle([FromRoute] int id)
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

            return vehicle;
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
              await  _service.UpdateVehicleAsync(vehicle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetVehicleAsync(id) ==null)
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
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {
            if (_service.GetAllVehicleAsync == null || vehicle == null)
            {
                return Problem("Entity set 'AppDbContext.Vehicles'  is null.");
            }

           await _service.CreateVehicleAsync(vehicle);

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute]int id)
        {
            if (_service.GetAllVehicleAsync == null || id <=0)
            {
                return NotFound();
            }
            var vehicle = await _service.GetVehicleAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

           await _service.DeleteVehicleAsync(vehicle);

            return NoContent();
        }

       
    }
}
