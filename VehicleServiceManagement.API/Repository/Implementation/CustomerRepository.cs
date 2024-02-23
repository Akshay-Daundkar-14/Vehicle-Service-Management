using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CustomerRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            var result = await _appDbContext.Customers.ToListAsync();
            return result;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var customer = await _appDbContext.Customers.FindAsync(id);
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _appDbContext.Entry(customer).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
