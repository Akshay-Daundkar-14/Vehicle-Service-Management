using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class ScheduledServiceRepository : IScheduledServiceRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ScheduledServiceRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateScheduledServiceAsync(ScheduledService scheduledService)
        {
            await _appDbContext.ScheduledServices.AddAsync(scheduledService);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteScheduledServiceAsync(ScheduledService scheduledService)
        {
            scheduledService.IsDeleted = true;
            _appDbContext.Entry(scheduledService).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ScheduledService>> GetAllScheduledServiceAsync()
        {
            var result = await _appDbContext.ScheduledServices.ToListAsync();
            return result;
        }

        public async Task<List<ScheduledService>> GetAllScheduledServiceAsync(int serviceRepresentativeId)
        {
            var result = await _appDbContext.ScheduledServices.Where(s=>s.ServiceAdvisorID == serviceRepresentativeId).ToListAsync();
            return result;
        }

        public async Task<ScheduledService> GetScheduledServiceAsync(int id)
        {
            var scheduledService = await _appDbContext.ScheduledServices.FindAsync(id);
            return scheduledService;
        }

        public async Task UpdateScheduledServiceAsync(ScheduledService scheduledService)
        {
            _appDbContext.Entry(scheduledService).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
