using Microsoft.AspNetCore.Mvc;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RequestRegisterDto request)
        {
            return Task.FromResult<IActionResult>(Ok());
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseLoginDto>> Login([FromBody] RequestLoginDto request)
        {
            var response = await _authService.LoginAsync(request);
            if (response.Token == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseLoginDto>> RefreshToken([FromBody] RequestRefreshTokenDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);
            if (response.Token == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
