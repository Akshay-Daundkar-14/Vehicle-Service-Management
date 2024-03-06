using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public MaterialRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateMaterialAsync(Material material)
        {
            await _appDbContext.Materials.AddAsync(material);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteMaterialAsync(Material material)
        {
            material.IsDeleted = true;
            _appDbContext.Entry(material).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Material>> GetAllMaterialAsync()
        {
            var result = await _appDbContext.Materials.Where(m=>m.IsDeleted==false).ToListAsync();
            return result;
        }

        public async Task<Material> GetMaterialAsync(int id)
        {
            var material = await _appDbContext.Materials.FindAsync(id);
            return material;
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            _appDbContext.Entry(material).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
