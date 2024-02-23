using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllCustomerAsync();

        public Task<Customer> GetCustomerAsync(int id);


        public Task CreateCustomerAsync(Customer customer);

        public Task UpdateCustomerAsync(Customer customer);


        public Task DeleteCustomerAsync(Customer customer);
    }
}
