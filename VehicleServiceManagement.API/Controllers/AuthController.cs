using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public string Login(User user)
        {


            // Need to write a logic to validate user and then generate the token

            return "User Login Successfully";
        }
    }
}
