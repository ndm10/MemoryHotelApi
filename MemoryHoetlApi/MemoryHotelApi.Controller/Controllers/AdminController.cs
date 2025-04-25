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
        private readonly ISubTourService _subTourService;
        private readonly IBranchService _branchService;
        private readonly IConvenienceService _convenienceService;
        private readonly IRoomCategoryService _roomCategoryService;

        public AdminController(IBannerService bannerService, IStoryService storyService, ICityService cityService, ITourService tourService, ISubTourService subTourService, IBranchService branchService, IConvenienceService convenienceService, IRoomCategoryService roomCategoryService)
        {
            _bannerService = bannerService;
            _storyService = storyService;
            _cityService = cityService;
            _tourService = tourService;
            _subTourService = subTourService;
            _branchService = branchService;
            _convenienceService = convenienceService;
            _roomCategoryService = roomCategoryService;
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
        public async Task<ActionResult<BaseResponseDto>> UploadBanner(RequestUploadBannerDto request)
        {
            var response = await _bannerService.UploadBannerAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("banner/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateBanner(RequestUpdateBannerDto request, Guid id)
        {
            var response = await _bannerService.UpdateBannerAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("banner/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteBanner(Guid id)
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
        public async Task<ActionResult<BaseResponseDto>> UploadStory(RequestUploadStoryDto request)
        {
            var response = await _storyService.UploadStoryAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("story/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateStory(RequestUpdateStoryDto request, Guid id)
        {
            var response = await _storyService.UpdateStoryAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("story/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteStory(Guid id)
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
        public async Task<ActionResult<BaseResponseDto>> UploadCity(RequestUploadCityDto request)
        {
            var response = await _cityService.UploadCityAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("city/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateCity(RequestUpdateCityDto request, Guid id)
        {
            var response = await _cityService.UpdateCityAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("city/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteCity(Guid id)
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
        public async Task<ActionResult<BaseResponseDto>> UploadTour(RequestUploadTourDto request)
        {
            var response = await _tourService.UploadTourAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("tour/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateTour(RequestUpdateTourDto request, Guid id)
        {
            var response = await _tourService.UpdateTourAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("tour/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteTour(Guid id)
        {
            var response = await _tourService.SoftDeleteTourAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("subtour")]
        public async Task<ActionResult<ResponseGetSubToursDto>> GetSubTours(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? tourId)
        {
            var response = await _subTourService.GetSubToursAsync(pageIndex, pageSize, textSearch, status, tourId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("subtour/{id}")]
        public async Task<ActionResult<ResponseGetSubToursDto>> GetSubTour(Guid id)
        {
            var response = await _subTourService.GetSubTourAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("subtour")]
        public async Task<ActionResult<BaseResponseDto>> UploadSubTour(RequestUploadSubTourDto request)
        {
            var response = await _subTourService.UploadSubTourAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("subtour/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateSubTour(RequestUpdateSubTourDto request, Guid id)
        {
            var response = await _subTourService.UpdateSubTourAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("subtour/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteSubTour(Guid id)
        {
            var response = await _subTourService.SoftDeleteSubTourAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("convenience")]
        public async Task<ActionResult<ResponseGetConveniencesDto>> GetConveniences(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _convenienceService.GetConveniencesAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("convenience/{id}")]
        public async Task<ActionResult<ResponseGetConvenienceDto>> GetConvenience(Guid id)
        {
            var response = await _convenienceService.GetConvenienceAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("convenience")]
        public async Task<ActionResult<BaseResponseDto>> UploadConvenience(RequestUploadConvenienceDto request)
        {
            var response = await _convenienceService.UploadConvenienceAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("convenience/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateConvenience(RequestUpdateConvenienceDto request, Guid id)
        {
            var response = await _convenienceService.UpdateConvenienceAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("convenience/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteConvenience(Guid id)
        {
            var response = await _convenienceService.SoftDeleteConvenienceAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("branch")]
        public async Task<ActionResult<ResponseGetBranchesDto>> GetBranches(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _branchService.GetBranchesAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("branch/{id}")]
        public async Task<ActionResult<ResponseGetBranchDto>> GetBranch(Guid id)
        {
            var response = await _branchService.GetBranchAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("branch")]
        public async Task<ActionResult<BaseResponseDto>> UploadBranch(RequestUploadBranchDto request)
        {
            var response = await _branchService.UploadBranchAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("branch/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateBranch(RequestUpdateBranchDto request, Guid id)
        {
            var response = await _branchService.UpdateBranchAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("branch/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteBranch(Guid id)
        {
            var response = await _branchService.SoftDeleteBranchAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("roomcategory")]
        public async Task<ActionResult<ResponseGetRoomCategoriesDto>> GetRoomCategories(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _roomCategoryService.GetRoomCategoriesAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("roomcategory/{id}")]
        public async Task<ActionResult<ResponseGetRoomCategoryDto>> GetRoomCategory(Guid id)
        {
            var response = await _roomCategoryService.GetRoomCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("roomcategory")]
        public async Task<ActionResult<BaseResponseDto>> UploadRoomCategory(RequestUploadRoomCategoryDto request)
        {
            var response = await _roomCategoryService.UploadRoomCategoryAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("roomcategory/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateRoomCategory(RequestUpdateRoomCategoryDto request, Guid id)
        {
            var response = await _roomCategoryService.UpdateRoomCategoryAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("roomcategory/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteRoomCategory(Guid id)
        {
            var response = await _roomCategoryService.SoftDeleteRoomCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
