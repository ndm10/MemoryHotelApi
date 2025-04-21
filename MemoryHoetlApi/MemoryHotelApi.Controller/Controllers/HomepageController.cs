using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.HomepageDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomepageController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public HomepageController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet("banners")]
        public async Task<ActionResult<ResponseGetBannersHomepageDto>> GetBanners()
        {
            var response = await _bannerService.GetAllBannersAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
