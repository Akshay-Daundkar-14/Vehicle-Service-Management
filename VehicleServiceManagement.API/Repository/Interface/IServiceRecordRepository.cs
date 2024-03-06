using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IServiceRecordRepository
    {
        public Task<List<ServiceRecord>> GetAllServiceRecordAsync();

        public Task<ServiceRecord> GetServiceRecordAsync(int id);

        public Task<ServiceRecord> GetServiceRecordByVehicleIdAsync(int vehicleId);


        public Task CreateServiceRecordAsync(ServiceRecord serviceRecord);

        public Task UpdateServiceRecordAsync(ServiceRecord serviceRecord);


        public Task DeleteServiceRecordAsync(ServiceRecord serviceRecord);
    }
}
