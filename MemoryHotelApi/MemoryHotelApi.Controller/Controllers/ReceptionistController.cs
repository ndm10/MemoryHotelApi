using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.Receptionist;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Receptionist, Admin")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IFoodOrderHistoryService _foodOrderHistoryService;

        public ReceptionistController(IFoodOrderHistoryService foodOrderHistoryService)
        {
            _foodOrderHistoryService = foodOrderHistoryService;
        }

        [HttpGet("order")]
        public async Task<ActionResult<ResponseGetFoodOrderHistoriesDto>> GetFoodOrderHistories(int? pageIndex, int? pageSize, string? textSearch, string? orderStatus, Guid? branchId)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Something error when authenticating!"
                });
            }

            var response = await _foodOrderHistoryService.GetFoodOrderHistoriesAsync(pageIndex, pageSize, textSearch, orderStatus, Guid.Parse(userId), branchId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult<ResponseGetFoodOrderHistoryDto>> GetFoodOrderHistoryByIdAsync(Guid id)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Something error when authenticating!"
                });
            }

            var response = await _foodOrderHistoryService.GetFoodOrderHistoryAsync(userId, id);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("order")]
        public async Task<ActionResult<BaseResponseDto>> CreateOrderAsync([FromBody] RequestCreateOrderDto request)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Something error when authenticating!"
                });
            }

            var response = await _foodOrderHistoryService.CreateOrderAsync(request, Guid.Parse(userId));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("order/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateOrderAsync(Guid id, [FromBody] RequestUpdateOrderDto request)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Something error when authenticating!"
                });
            }
            var response = await _foodOrderHistoryService.UpdateOrderAsync(id, request, Guid.Parse(userId));
            return StatusCode(response.StatusCode, response);
        }
    }
}
