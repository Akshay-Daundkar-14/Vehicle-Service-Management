using Microsoft.EntityFrameworkCore;
using VehicleServiceManagement.API.Data;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class ServerErrorRepository : IServerErrorRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public ServerErrorRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateErrorAsync(Error Error)
        {
            await _appDbContext.Errors.AddAsync(Error);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
