using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IScheduledServiceRepository
    {
        public Task<List<ScheduledService>> GetAllScheduledServiceAsync();

        public Task<ScheduledService> GetScheduledServiceAsync(int id);


        public Task CreateScheduledServiceAsync(ScheduledService ScheduledService);

        public Task UpdateScheduledServiceAsync(ScheduledService ScheduledService);


        public Task DeleteScheduledServiceAsync(ScheduledService ScheduledService);

        public Task<List<ScheduledService>> GetAllScheduledServiceAsync(int serviceRepresentativeId);

    }
}
