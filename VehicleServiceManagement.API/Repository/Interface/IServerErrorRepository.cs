using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface IServerErrorRepository
    {
        public Task CreateErrorAsync(Error Error);

    }
}
