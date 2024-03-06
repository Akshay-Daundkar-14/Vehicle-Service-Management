using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public VehicleRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateVehicleAsync(Vehicle vehicle)
        {
            await _appDbContext.Vehicles.AddAsync(vehicle);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            vehicle.IsDeleted = true;
            _appDbContext.Entry(vehicle).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicleAsync()
        {
            var result = await _appDbContext.Vehicles.Where(v=>v.IsDeleted==false).ToListAsync();
            return result;
        }

        public async Task<Vehicle> GetVehicleAsync(int id)
        {
            var vehicle = await _appDbContext.Vehicles.FindAsync(id);
            return vehicle;
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            _appDbContext.Entry(vehicle).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
