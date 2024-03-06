using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IServiceRepresentativeRepository
    {
        public Task<List<ServiceRepresentative>> GetAllServiceRepresentativeAsync();

        public Task<ServiceRepresentative> GetServiceRepresentativeAsync(int id);

        public Task<ServiceRepresentative> GetServiceRepresentativeByEmailAsync(string email);


        public Task CreateServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative);

        public Task UpdateServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative);


        public Task DeleteServiceRepresentativeAsync(ServiceRepresentative serviceRepresentative);
    }
}
