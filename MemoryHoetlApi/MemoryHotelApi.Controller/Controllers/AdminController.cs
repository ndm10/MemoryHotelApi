using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public AdminController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet("banner")]
        public async Task<IActionResult> GetBanners(int pageIndex, int pageSize, string textSearch, bool status)
        {
            // Implement your logic to get banners here
            // For example, you can call a service to fetch the banners from the database
            // and return them as a response.
            // Placeholder response
            var banners = new List<string> { "Banner1", "Banner2", "Banner3" };
            return Ok(banners);
        }

        [HttpPost("banner")]
        public async Task<ActionResult<GenericResponseDto>> UploadBanner(RequestUploadBannerDto request)
        {
            var response = await _bannerService.UploadBanner(request);

            if(response.IsError == false)
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("banner")]
        public async Task<IActionResult> UpdateBanner()
        {
            // Implement your logic to upload the banner here
            // For example, you can save the file to a specific location and return a success response.
            // Placeholder response
            return Ok(new { Message = "Banner uploaded successfully." });
        }

        [HttpDelete("banner/{id}")]
        public async Task<ActionResult<GenericResponseDto>> DeleteBanner(Guid id)
        {
            var response = await _bannerService.SoftDeleteAsync(id);

            if (response.IsError == false)
            {
                return StatusCode(500, response);
            }

            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
