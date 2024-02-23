using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IMaterialRepository
    {
        public Task<List<Material>> GetAllMaterialAsync();

        public Task<Material> GetMaterialAsync(int id);


        public Task CreateMaterialAsync(Material material);

        public Task UpdateMaterialAsync(Material material);


        public Task DeleteMaterialAsync(Material material);
    }
}
