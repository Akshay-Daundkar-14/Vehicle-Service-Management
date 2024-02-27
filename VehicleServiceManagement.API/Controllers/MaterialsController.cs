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
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialRepository _service;
        private readonly ILogger<MaterialsController> _logger;

        public MaterialsController(IMaterialRepository service, ILogger<MaterialsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            var materialDomain = await _service.GetAllMaterialAsync();
            if (materialDomain == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Material\" retrieved successfully");
            return Ok(materialDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial([FromRoute] int id)
        {

            if (id <= 0)
            {
                return NotFound();
            }
            var material = await _service.GetMaterialAsync(id);

            if (material == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"\"Material\" retrieved successfully with id -> {id}");
            return material;

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial([FromRoute] int id, [FromBody] Material material)
        {
            if (id != material.ItemID || material == null)
            {
                return BadRequest();
            }

            await _service.UpdateMaterialAsync(material);
            _logger.LogInformation($"\"Material\" with id -> {id} updated successfully.");


            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial([FromBody] Material material)
        {

            if (material == null)
            {
                return Problem("Entity set 'AppDbContext.Materials'  is null.");
            }

            await _service.CreateMaterialAsync(material);
            _logger.LogInformation($"\"Material\" created successfully with id -> {material.ItemID}");
            return CreatedAtAction("GetMaterial", new { id = material.ItemID }, material);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {

            if (id <= 0)
            {
                return NotFound();
            }
            var material = await _service.GetMaterialAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            await _service.DeleteMaterialAsync(material);
            _logger.LogInformation($"\"Material\" deleted successfully with id -> {material.ItemID}");
            return NoContent();

        }


    }
}
