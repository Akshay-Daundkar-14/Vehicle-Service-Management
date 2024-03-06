using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class ServiceRepresentativeRepository : IServiceRepresentativeRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ServiceRepresentativeRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative)
        {
            await _appDbContext.ServiceRepresentatives.AddAsync(serviceRepresentative);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative)
        {
            serviceRepresentative.IsDeleted = true;
            _appDbContext.Entry(serviceRepresentative).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ServiceRepresentative>> GetAllServiceRepresentativeAsync()
        {
            var result = await _appDbContext.ServiceRepresentatives.Where(s=>s.IsDeleted==false).ToListAsync();
            return result;
        }

        public async Task<ServiceRepresentative> GetServiceRepresentativeAsync(int id)
        {
            var serviceRepresentative = await _appDbContext.ServiceRepresentatives.FindAsync(id);
            return serviceRepresentative;
        }

        public async Task<ServiceRepresentative> GetServiceRepresentativeByEmailAsync(string email)
        {
            var serviceRepresentative = await _appDbContext.ServiceRepresentatives.FirstOrDefaultAsync(s => s.Email == email);
            return serviceRepresentative;
        }

        public async Task UpdateServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative)
        {
            _appDbContext.Entry(serviceRepresentative).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
