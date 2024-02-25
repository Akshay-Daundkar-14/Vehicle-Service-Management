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
    //[Authorize(Roles = "Admin")]
    public class ScheduledServicesController : ControllerBase
    {
        private readonly IScheduledServiceRepository _service;

        public ScheduledServicesController(IScheduledServiceRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledService>>> GetScheduledServices()
        {
            var scheduledServiceDomain = await _service.GetAllScheduledServiceAsync();
            if (scheduledServiceDomain == null)
            {
                return NotFound();
            }

            return Ok(scheduledServiceDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduledService>> GetScheduledService([FromRoute] int id)
        {
            if (_service.GetAllScheduledServiceAsync == null || id <= 0)
            {
                return NotFound();
            }
            var scheduledServiceDomain = await _service.GetScheduledServiceAsync(id);

            if (scheduledServiceDomain == null)
            {
                return NotFound();
            }

            return scheduledServiceDomain;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduledService([FromRoute]int id, [FromBody]ScheduledService scheduledServiceDomain)
        {
            if (id != scheduledServiceDomain.ScheduledServiceId || scheduledServiceDomain == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateScheduledServiceAsync(scheduledServiceDomain);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetScheduledServiceAsync(id) ==null)
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
        public async Task<ActionResult<ScheduledService>> PostScheduledService([FromBody] ScheduledService scheduledServiceDomain)
        {
            if (_service.GetAllScheduledServiceAsync == null || scheduledServiceDomain == null)
            {
                return Problem("Entity set 'AppDbContext.ScheduledServices'  is null.");
            }

           await _service.CreateScheduledServiceAsync(scheduledServiceDomain);

            return CreatedAtAction("GetScheduledService", new { id = scheduledServiceDomain.ScheduledServiceId }, scheduledServiceDomain);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduledService([FromRoute]int id)
        {
            if (_service.GetAllScheduledServiceAsync == null || id <=0)
            {
                return NotFound();
            }
            var scheduledServiceDomain = await _service.GetScheduledServiceAsync(id);
            if (scheduledServiceDomain == null)
            {
                return NotFound();
            }

           await _service.DeleteScheduledServiceAsync(scheduledServiceDomain);

            return NoContent();
        }

       
    }
}
