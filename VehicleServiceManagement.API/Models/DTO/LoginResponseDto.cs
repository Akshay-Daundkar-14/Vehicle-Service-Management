namespace VehicleServiceManagement.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; } // if multiple roles assign to one user then List<string>
    }
}
