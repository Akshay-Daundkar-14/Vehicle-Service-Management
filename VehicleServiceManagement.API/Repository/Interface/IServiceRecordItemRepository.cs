using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IServiceRecordItemRepository
    {
        public Task<List<ServiceRecordItem>> GetAllServiceRecordItemAsync();

        public Task<ServiceRecordItem> GetServiceRecordItemAsync(int id);


        public Task CreateServiceRecordItemAsync(ServiceRecordItem serviceRecordItem);

        public Task UpdateServiceRecordItemAsync(ServiceRecordItem serviceRecordItem);


        public Task DeleteServiceRecordItemAsync(ServiceRecordItem serviceRecordItem);
    }
}
