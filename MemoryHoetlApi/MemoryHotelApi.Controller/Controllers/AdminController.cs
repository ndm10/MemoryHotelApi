using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
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
        private readonly IStoryService _storyService;
        private readonly ICityService _cityService;
        private readonly ITourService _tourService;
        public readonly ISubTourService _subTourService;

        public AdminController(IBannerService bannerService, IStoryService storyService, ICityService cityService, ITourService tourService, ISubTourService subTourService)
        {
            _bannerService = bannerService;
            _storyService = storyService;
            _cityService = cityService;
            _tourService = tourService;
            _subTourService = subTourService;
        }

        [HttpGet("banner")]
        public async Task<ActionResult<ResponseGetBannersDto>> GetBanners(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _bannerService.GetBannersAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("banner/{id}")]
        public async Task<ActionResult<ResponseGetBannersDto>> GetBanner(Guid id)
        {
            var response = await _bannerService.GetBannerAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("banner")]
        public async Task<ActionResult<GenericResponseDto>> UploadBanner(RequestUploadBannerDto request)
        {
            var response = await _bannerService.UploadBannerAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("banner/{id}")]
        public async Task<ActionResult<GenericResponseDto>> UpdateBanner(RequestUpdateBannerDto request, Guid id)
        {
            var response = await _bannerService.UpdateBannerAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("banner/{id}")]
        public async Task<ActionResult<GenericResponseDto>> DeleteBanner(Guid id)
        {
            var response = await _bannerService.SoftDeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("story")]
        public async Task<ActionResult<ResponseGetBannersDto>> GetStories(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _storyService.GetStoriesAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("story/{id}")]
        public async Task<ActionResult<ResponseGetBannersDto>> GetStory(Guid id)
        {
            var response = await _storyService.GetStoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("story")]
        public async Task<ActionResult<GenericResponseDto>> UploadStory(RequestUploadStoryDto request)
        {
            var response = await _storyService.UploadStoryAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("story/{id}")]
        public async Task<ActionResult<GenericResponseDto>> UpdateStory(RequestUpdateStoryDto request, Guid id)
        {
            var response = await _storyService.UpdateStoryAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("story/{id}")]
        public async Task<ActionResult<GenericResponseDto>> DeleteStory(Guid id)
        {
            var response = await _storyService.SoftDeleteStoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("city")]
        public async Task<ActionResult<ResponseGetCitiesDto>> GetCities(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _cityService.GetCities(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("city/{id}")]
        public async Task<ActionResult<ResponseGetCityDto>> GetCity(Guid id)
        {
            var response = await _cityService.GetCity(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("city")]
        public async Task<ActionResult<GenericResponseDto>> UploadCity(RequestUploadCityDto request)
        {
            var response = await _cityService.UploadCityAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("city/{id}")]
        public async Task<ActionResult<GenericResponseDto>> UpdateCity(RequestUpdateCityDto request, Guid id)
        {
            var response = await _cityService.UpdateCityAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("city/{id}")]
        public async Task<ActionResult<GenericResponseDto>> DeleteCity(Guid id)
        {
            var response = await _cityService.SoftDeleteCityAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("tour")]
        public async Task<ActionResult<ResponseGetToursDto>> GetTours(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? cityId)
        {
            var response = await _tourService.GetToursAsync(pageIndex, pageSize, textSearch, status, cityId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("tour/{id}")]
        public async Task<ActionResult<ResponseGetTourDto>> GetTour(Guid id)
        {
            var response = await _tourService.GetTourAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("tour")]
        public async Task<ActionResult<GenericResponseDto>> UploadTour(RequestUploadTourDto request)
        {
            var response = await _tourService.UploadTourAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("tour/{id}")]
        public async Task<ActionResult<GenericResponseDto>> UpdateTour(RequestUpdateTourDto request, Guid id)
        {
            var response = await _tourService.UpdateTourAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("tour/{id}")]
        public async Task<ActionResult<GenericResponseDto>> DeleteTour(Guid id)
        {
            var response = await _tourService.SoftDeleteTourAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
