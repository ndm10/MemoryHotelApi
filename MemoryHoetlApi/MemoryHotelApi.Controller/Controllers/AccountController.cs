using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<ActionResult<ResponseUpdateProfileDto>> UpdateProfile(RequestUpdateProfileDto request)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseUpdateProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi xảy ra trong quá trình xác thực tài khoản!"
                });
            }

            request.UserId = Guid.Parse(userId);

            // Change password
            var response = await _accountService.UpdateProfile(request);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPatch("change-password")]
        public async Task<ActionResult<ResponseChangePasswordDto>> ChangePassword(RequestChangePasswordDto request)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseUpdateProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi xảy ra trong quá trình xác thực tài khoản!"
                });
            }

            request.UserId = Guid.Parse(userId);

            // Change password
            var response = await _accountService.ChangePassword(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
