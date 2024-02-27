using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceManagement.API.Models.Domain;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorHandlersController : ControllerBase // This controller will track all the angular errors
    {
        [HttpPost]
        public string LogError(Error errorHandler)
        {


            // Need to write a logic to Post errors in sql database

            return "Error Logged";
        }
    }
}
