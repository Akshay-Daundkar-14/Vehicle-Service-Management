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
    public class ServiceRecordItemsController : ControllerBase
    {
        private readonly IServiceRecordItemRepository _service;

        public ServiceRecordItemsController(IServiceRecordItemRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecordItem>>> GetServiceRecordItems()
        {
            var serviceRecordItem = await _service.GetAllServiceRecordItemAsync();
            if (serviceRecordItem == null)
            {
                return NotFound();
            }

            return Ok(serviceRecordItem);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRecordItem>> GetServiceRecordItem([FromRoute] int id)
        {
            if (_service.GetAllServiceRecordItemAsync == null || id <= 0)
            {
                return NotFound();
            }
            var serviceRecordItem = await _service.GetServiceRecordItemAsync(id);

            if (serviceRecordItem == null)
            {
                return NotFound();
            }

            return serviceRecordItem;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRecordItem([FromRoute]int id, [FromBody]ServiceRecordItem serviceRecordItem)
        {
            if (id != serviceRecordItem.ServiceRecordItemId || serviceRecordItem == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateServiceRecordItemAsync(serviceRecordItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetServiceRecordItemAsync(id) ==null)
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
        public async Task<ActionResult<ServiceRecordItem>> PostServiceRecordItem([FromBody] ServiceRecordItem serviceRecordItem)
        {
            if (_service.GetAllServiceRecordItemAsync == null || serviceRecordItem == null)
            {
                return Problem("Entity set 'AppDbContext.ServiceRecordItems'  is null.");
            }

           await _service.CreateServiceRecordItemAsync(serviceRecordItem);

            return CreatedAtAction("GetServiceRecordItem", new { id = serviceRecordItem.ServiceRecordItemId }, serviceRecordItem);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecordItem([FromRoute]int id)
        {
            if (_service.GetAllServiceRecordItemAsync == null || id <=0)
            {
                return NotFound();
            }
            var serviceRecordItem = await _service.GetServiceRecordItemAsync(id);
            if (serviceRecordItem == null)
            {
                return NotFound();
            }

           await _service.DeleteServiceRecordItemAsync(serviceRecordItem);

            return NoContent();
        }

       
    }
}
