using Microsoft.AspNetCore.Identity;

namespace VehicleServiceManagement.API.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user,string role);
    }
}
