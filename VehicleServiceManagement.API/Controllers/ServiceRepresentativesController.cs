using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ServiceRepresentativesController : ControllerBase
    {
        private readonly IServiceRepresentativeRepository _service;
        private readonly ILogger<ServiceRepresentativesController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public ServiceRepresentativesController(UserManager<IdentityUser> userManager,IServiceRepresentativeRepository service, ILogger<ServiceRepresentativesController> logger)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ServiceRepresentative>>> GetServiceRepresentatives()
        {

            var serviceRepresentativeDomain = await _service.GetAllServiceRepresentativeAsync();
            if (serviceRepresentativeDomain == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Service Representative\" retrieved successfully");
            return Ok(serviceRepresentativeDomain);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceRepresentative>> GetServiceRepresentative([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var serviceRepresentative = await _service.GetServiceRepresentativeAsync(id);

            if (serviceRepresentative == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Service Representative\" retrieved successfully with id -> {id}");
            return serviceRepresentative;

        }


        [HttpGet("byEmail/{email}")]
        //[Authorize(Roles = "Service Advisor")]
        public async Task<ActionResult<ServiceRepresentative>> GetServiceRepresentativeByEmail([FromRoute] string email)
        {

            if (email == null)
            {
                return BadRequest();
            }
            var serviceRepresentative = await _service.GetServiceRepresentativeByEmailAsync(email);

            if (serviceRepresentative == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Service Representative\" retrieved successfully with email -> {email}");
            return serviceRepresentative;

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutServiceRepresentative([FromRoute] int id, [FromBody] ServiceRepresentative serviceRepresentative)
        {
            if (id != serviceRepresentative.RepresentativeID || serviceRepresentative == null)
            {
                return BadRequest();
            }


            await _service.UpdateServiceRepresentativeAsync(serviceRepresentative);
            _logger.LogInformation($"\"Service Representative\" with id -> {id} updated successfully.");


            return NoContent();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceRepresentative>> PostServiceRepresentative([FromBody] ServiceRepresentative serviceRepresentative)
        {

            if (serviceRepresentative == null)
            {
                return Problem("Entity set 'AppDbContext.ServiceRepresentatives'  is null.");
            }

            var user = new IdentityUser()
            {
                Email = serviceRepresentative.Email?.Trim(),
                UserName = serviceRepresentative.Email?.Trim(),
            };

            var identityResult = await _userManager.CreateAsync(user, "Sa@123");

            if (identityResult.Succeeded)
            {
                // Add Role to user

                identityResult = await _userManager.AddToRoleAsync(user, "Service Advisor");

                if (identityResult.Succeeded)
                {
                    _logger.LogInformation($"New service advisor registered successfully");
                    await _service.CreateServiceRepresentativeAsync(serviceRepresentative);
                    _logger.LogInformation($"\"Service Representative\" created successfully with id -> {serviceRepresentative.RepresentativeID}");
                    return CreatedAtAction("GetServiceRepresentative", new { id = serviceRepresentative.RepresentativeID }, serviceRepresentative);

                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);

        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteServiceRepresentative([FromRoute] int id)
        {

            if (id <= 0)
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


    }
}
