using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IVehicleRepository
    {
        public Task<List<Vehicle>> GetAllVehicleAsync();

        public Task<Vehicle> GetVehicleAsync(int id);


        public Task CreateVehicleAsync(Vehicle Vehicle);

        public Task UpdateVehicleAsync(Vehicle Vehicle);


        public Task DeleteVehicleAsync(Vehicle Vehicle);
    }
}
