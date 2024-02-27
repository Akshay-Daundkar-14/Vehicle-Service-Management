using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceManagement.API.Models.Domain;
using VehicleServiceManagement.API.Models.DTO;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            // No Need to write logic to check whether the email is already register or not because identity internally checks it.

            try
            {
                var user = new IdentityUser()
                {
                    Email = request.Email?.Trim(),
                    UserName = request.Email?.Trim(),
                };

                var identityResult = await _userManager.CreateAsync(user, request.Password);

                if (identityResult.Succeeded)
                {
                    // Add Role to user

                    identityResult = await _userManager.AddToRoleAsync(user, "Customer");

                    if (identityResult.Succeeded)
                    {
                        _logger.LogInformation($"New user registered successfully");
                        return Ok();
                    }
                    else
                    {
                        if (identityResult.Errors.Any())
                        {
                            foreach (var error in identityResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Registering User : {ex.Message}");
                return StatusCode(500, "Internal server server");
            }  
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequestDto request)
        {
            // Check Email 

            try
            {
                var identityUser = await _userManager.FindByEmailAsync(request.Email);

                if (identityUser is not null)
                {
                    bool isPasswordMatched = await _userManager.CheckPasswordAsync(identityUser, request.Password);

                    if (isPasswordMatched)
                    {
                        // generate token & response


                        var roles = await _userManager.GetRolesAsync(identityUser);
                        var role = roles.ElementAtOrDefault(0);

                        var jwtToken = _tokenRepository.CreateJwtToken(identityUser, role);

                        var response = new LoginResponseDto()
                        {
                            Email = request.Email,
                            Role = role,
                            Token = jwtToken
                        };
                        _logger.LogInformation($"User logged in successfully");
                        return Ok(response);
                    }
                }

                ModelState.AddModelError("", "Email or Password Incorrect");

                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while login user: {ex.Message}");
                return StatusCode(500, "Internal server server");
            }

        }
    }
}
