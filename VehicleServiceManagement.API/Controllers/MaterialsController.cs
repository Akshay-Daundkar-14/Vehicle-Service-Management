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

        public MaterialsController(IMaterialRepository service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            var materialDomain = await _service.GetAllMaterialAsync();
            if (materialDomain == null)
            {
                return NotFound();
            }

            //var MaterialDto = new List<MaterialDTO>();

            //foreach (var MaterialDomains in MaterialDomain)
            //{
            //    MaterialDto.Add(new MaterialDTO()
            //    {
            //        MaterialId = MaterialDomains.MaterialId,
            //        FirstName = MaterialDomains.FirstName,
            //        LastName = MaterialDomains.LastName,
            //        Email = MaterialDomains.Email,
            //        Password = MaterialDomains.Password,
            //        Address = MaterialDomains.Address,
            //        Mobile = MaterialDomains.Mobile,
            //        Image = MaterialDomains.Image,
            //        IsDeleted = MaterialDomains.IsDeleted,
            //        CreatedDate = MaterialDomains.CreatedDate,
            //        UpdatedDate = MaterialDomains.UpdatedDate,
            //    });
            //}
            return Ok(materialDomain);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial([FromRoute] int id)
        {
            if (_service.GetAllMaterialAsync == null || id <= 0)
            {
                return NotFound();
            }
            var material = await _service.GetMaterialAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial([FromRoute]int id, [FromBody]Material material)
        {
            if (id != material.ItemID || material == null)
            {
                return BadRequest();
            }

            try
            {
              await  _service.UpdateMaterialAsync(material);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_service.GetMaterialAsync(id) ==null)
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
        public async Task<ActionResult<Material>> PostMaterial([FromBody] Material material)
        {
            if (_service.GetAllMaterialAsync == null || material == null)
            {
                return Problem("Entity set 'AppDbContext.Materials'  is null.");
            }

           await _service.CreateMaterialAsync(material);

            return CreatedAtAction("GetMaterial", new { id = material.ItemID }, material);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial([FromRoute]int id)
        {
            if (_service.GetAllMaterialAsync == null || id <=0)
            {
                return NotFound();
            }
            var material = await _service.GetMaterialAsync(id);
            if (material == null)
            {
                return NotFound();
            }

           await _service.DeleteMaterialAsync(material);

            return NoContent();
        }

       
    }
}
