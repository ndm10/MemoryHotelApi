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

            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseLoginDto>> Login([FromBody] RequestLoginDto request)
        {
            var response = await _authService.LoginAsync(request);
            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseLoginDto>> RefreshToken([FromBody] RequestRefreshTokenDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);
            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<ResponseSendOtpDto>> SendOtp(RequestSendOtpDto request)
        {
            try
            {
                string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

                // Assign the client Ip
                request.ClientIp = clientIp;

                var response = await _authService.SendOtpAsync(request);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult<VerifyOtpResponseDto>> VerifyOtp(RegisterVerifyOtpRequestDto request)
        {
            try
            {
                string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

                // Assign the client Ip
                request.ClientIp = clientIp;

                var response = await _authService.VerifyOtpAsync(request);

                if (response.IsSuccess == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("new-password")]
        public async Task<ActionResult<ResponseLoginDto>> NewPassword(RequestSetPasswordDto request)
        {
            try
            {
                string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

                // Assign the client Ip
                request.ClientIp = clientIp;

                var response = await _authService.SetNewPassword(request);

                if (response.IsSuccess == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("reset-password")]
        public async Task<ActionResult<ResponseResetPasswordDto>> ResetPassword(RequestResetPasswordDto request)
        {
            try
            {
                string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

                // Assign the client Ip
                request.ClientIp = clientIp;

                var response = await _authService.ResetPassword(request);

                if (response.IsSuccess == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
