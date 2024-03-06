using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class ServiceRecordRepository : IServiceRecordRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ServiceRecordRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateServiceRecordAsync(ServiceRecord serviceRecord)
        {
            await _appDbContext.ServiceRecords.AddAsync(serviceRecord);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteServiceRecordAsync(ServiceRecord serviceRecord)
        {
            serviceRecord.IsDeleted = true;
            _appDbContext.Entry(serviceRecord).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ServiceRecord>> GetAllServiceRecordAsync()
        {
            var result = await _appDbContext.ServiceRecords.ToListAsync();
            return result;
        }

        public async Task<ServiceRecord> GetServiceRecordAsync(int id)
        {
            var serviceRecord = await _appDbContext.ServiceRecords.FindAsync(id);
            return serviceRecord;
        }

        public Task<ServiceRecord> GetServiceRecordByVehicleIdAsync(int vehicleId)
        {
            var serviceRecord = _appDbContext.ServiceRecords.FirstOrDefaultAsync(s => s.VehicleID == vehicleId);
            return serviceRecord;
        }

        public async Task UpdateServiceRecordAsync(ServiceRecord serviceRecord)
        {
            _appDbContext.Entry(serviceRecord).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
