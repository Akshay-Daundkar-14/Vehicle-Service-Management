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
using VehicleServiceManagement.API.Models.DTO;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ServiceRecordsController : ControllerBase
    {
        private readonly IServiceRecordRepository _service;

        public ServiceRecordsController(IServiceRecordRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecord>>> GetServiceRecords()
        {
            var serviceRecord = await _service.GetAllServiceRecordAsync();
            if (serviceRecord == null)
            {
                return NotFound();
            }

            return Ok(serviceRecord);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRecord>> GetServiceRecord([FromRoute] int id)
        {
            if (_service.GetAllServiceRecordAsync == null || id <= 0)
            {
                return NotFound();
            }
            var serviceRecord = await _service.GetServiceRecordAsync(id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            return serviceRecord;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRecord([FromRoute]int id, [FromBody]ServiceRecord serviceRecord)
        {
            if (id != serviceRecord.ServiceRecordID || serviceRecord == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateServiceRecordAsync(serviceRecord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetServiceRecordAsync(id) ==null)
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
        public async Task<ActionResult<ServiceRecord>> PostServiceRecord([FromBody] ServiceRecord serviceRecord)
        {
            if (_service.GetAllServiceRecordAsync == null || serviceRecord == null)
            {
                return Problem("Entity set 'AppDbContext.ServiceRecords'  is null.");
            }

           await _service.CreateServiceRecordAsync(serviceRecord);

            return CreatedAtAction("GetServiceRecord", new { id = serviceRecord.ServiceRecordID }, serviceRecord);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecord([FromRoute]int id)
        {
            if (_service.GetAllServiceRecordAsync == null || id <=0)
            {
                return NotFound();
            }
            var serviceRecord = await _service.GetServiceRecordAsync(id);
            if (serviceRecord == null)
            {
                return NotFound();
            }

           await _service.DeleteServiceRecordAsync(serviceRecord);

            return NoContent();
        }

       
    }
}
