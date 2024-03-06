using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class ServiceRecordItemRepository : IServiceRecordItemRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ServiceRecordItemRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateServiceRecordItemAsync(ServiceRecordItem serviceRecordItem)
        {
            await _appDbContext.ServiceRecordItems.AddAsync(serviceRecordItem);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteServiceRecordItemAsync(ServiceRecordItem serviceRecordItem)
        {
            serviceRecordItem.IsDeleted = true;
            _appDbContext.Entry(serviceRecordItem).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ServiceRecordItem>> GetAllServiceRecordItemAsync()
        {
            var result = await _appDbContext.ServiceRecordItems.ToListAsync();
            return result;
        }

        public async Task<List<ServiceRecordItem>> GetAllServiceRecordItemByServiceRecordAsync(int id)
        {
            var result = await _appDbContext.ServiceRecordItems.Where(s => s.ServiceRecordID == id).ToListAsync();
            return result;
        }

        public async Task<ServiceRecordItem> GetServiceRecordItemAsync(int id)
        {
            var serviceRecordItem = await _appDbContext.ServiceRecordItems.FindAsync(id);
            return serviceRecordItem;
        }

        public async Task UpdateServiceRecordItemAsync(ServiceRecordItem serviceRecordItem)
        {
            _appDbContext.Entry(serviceRecordItem).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
