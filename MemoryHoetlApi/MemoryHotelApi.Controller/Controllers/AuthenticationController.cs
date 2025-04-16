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
        public async Task<ActionResult<ResponseRegisterDto>> Register([FromBody] RequestRegisterDto request)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            // Assign the client Ip
            request.ClientIp = clientIp;

            var response = await _authService.RegisterAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseLoginDto>> Login([FromBody] RequestLoginDto request)
        {
            var response = await _authService.LoginAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseLoginDto>> RefreshToken([FromBody] RequestRefreshTokenDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("reset-password-send-otp")]
        public async Task<ActionResult<ResponseSendOtpDto>> SendOtp(RequestSendOtpDto request)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            // Assign the client Ip
            request.ClientIp = clientIp;

            var response = await _authService.SendOtpAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<ResponseVerifyOtpDto>> VerifyOtp(RegisterVerifyOtpRequestDto request)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            // Assign the client Ip
            request.ClientIp = clientIp;

            var response = await _authService.VerifyOtpAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("set-new-password")]
        public async Task<ActionResult<ResponseLoginDto>> NewPassword(RequestSetPasswordDto request)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            // Assign the client Ip
            request.ClientIp = clientIp;

            var response = await _authService.SetNewPassword(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("reset-password")]
        public async Task<ActionResult<ResponseResetPasswordDto>> ResetPassword(RequestResetPasswordDto request)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            // Assign the client Ip
            request.ClientIp = clientIp;

            var response = await _authService.ResetPassword(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
