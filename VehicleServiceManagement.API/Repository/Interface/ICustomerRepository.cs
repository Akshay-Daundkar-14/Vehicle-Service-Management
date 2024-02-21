using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync(); 
    }
}
