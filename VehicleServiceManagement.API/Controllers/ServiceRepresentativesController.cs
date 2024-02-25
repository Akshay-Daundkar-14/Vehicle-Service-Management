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

        public ServiceRepresentativesController(IServiceRepresentativeRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRepresentative>>> GetServiceRepresentatives()
        {
            var serviceRepresentativeDomain = await _service.GetAllServiceRepresentativeAsync();
            if (serviceRepresentativeDomain == null)
            {
                return NotFound();
            }

            //var ServiceRepresentativeDto = new List<ServiceRepresentativeDTO>();

            //foreach (var ServiceRepresentativeDomains in ServiceRepresentativeDomain)
            //{
            //    ServiceRepresentativeDto.Add(new ServiceRepresentativeDTO()
            //    {
            //        ServiceRepresentativeId = ServiceRepresentativeDomains.ServiceRepresentativeId,
            //        FirstName = ServiceRepresentativeDomains.FirstName,
            //        LastName = ServiceRepresentativeDomains.LastName,
            //        Email = ServiceRepresentativeDomains.Email,
            //        Password = ServiceRepresentativeDomains.Password,
            //        Address = ServiceRepresentativeDomains.Address,
            //        Mobile = ServiceRepresentativeDomains.Mobile,
            //        Image = ServiceRepresentativeDomains.Image,
            //        IsDeleted = ServiceRepresentativeDomains.IsDeleted,
            //        CreatedDate = ServiceRepresentativeDomains.CreatedDate,
            //        UpdatedDate = ServiceRepresentativeDomains.UpdatedDate,
            //    });
            //}
            return Ok(serviceRepresentativeDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRepresentative>> GetServiceRepresentative([FromRoute] int id)
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

            return serviceRepresentative;
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
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetServiceRepresentativeAsync(id) ==null)
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
        public async Task<ActionResult<ServiceRepresentative>> PostServiceRepresentative([FromBody] ServiceRepresentative serviceRepresentative)
        {
            if (_service.GetAllServiceRepresentativeAsync == null || serviceRepresentative == null)
            {
                return Problem("Entity set 'AppDbContext.ServiceRepresentatives'  is null.");
            }

           await _service.CreateServiceRepresentativeAsync(serviceRepresentative);

            return CreatedAtAction("GetServiceRepresentative", new { id = serviceRepresentative.RepresentativeID }, serviceRepresentative);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRepresentative([FromRoute]int id)
        {
            if (_service.GetAllServiceRepresentativeAsync == null || id <=0)
            {
                return NotFound();
            }
            var serviceRepresentative = await _service.GetServiceRepresentativeAsync(id);
            if (serviceRepresentative == null)
            {
                return NotFound();
            }

           await _service.DeleteServiceRepresentativeAsync(serviceRepresentative);

            return NoContent();
        }

       
    }
}
