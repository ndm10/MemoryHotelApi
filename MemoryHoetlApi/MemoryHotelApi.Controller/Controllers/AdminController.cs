using MemoryHotelApi.BusinessLogicLayer.Common;
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
        private readonly IUserService _userService;
        private readonly IMembershipTierService _membershipTierService;
        private readonly IMembershipTierBenefitService _membershipTierBenefitService;
        private readonly IRoomService _roomService;

        public AdminController(IBannerService bannerService, IStoryService storyService, ICityService cityService, ITourService tourService, ISubTourService subTourService, IBranchService branchService, IConvenienceService convenienceService, IRoomCategoryService roomCategoryService, IUserService userService, IMembershipTierService membershipTierService, IMembershipTierBenefitService membershipTierBenefitService, IRoomService roomService)
        {
            _bannerService = bannerService;
            _storyService = storyService;
            _cityService = cityService;
            _tourService = tourService;
            _subTourService = subTourService;
            _branchService = branchService;
            _convenienceService = convenienceService;
            _roomCategoryService = roomCategoryService;
            _userService = userService;
            _membershipTierService = membershipTierService;
            _membershipTierBenefitService = membershipTierBenefitService;
            _roomService = roomService;
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

        [HttpGet("room-category")]
        public async Task<ActionResult<ResponseGetRoomCategoriesDto>> GetRoomCategories(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _roomCategoryService.GetRoomCategoriesAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("room-category/{id}")]
        public async Task<ActionResult<ResponseGetRoomCategoryDto>> GetRoomCategory(Guid id)
        {
            var response = await _roomCategoryService.GetRoomCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("room-category")]
        public async Task<ActionResult<BaseResponseDto>> UploadRoomCategory(RequestUploadRoomCategoryDto request)
        {
            var response = await _roomCategoryService.UploadRoomCategoryAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("room-category/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateRoomCategory(RequestUpdateRoomCategoryDto request, Guid id)
        {
            var response = await _roomCategoryService.UpdateRoomCategoryAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("room-category/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteRoomCategory(Guid id)
        {
            var response = await _roomCategoryService.SoftDeleteRoomCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership")]
        public async Task<ActionResult<ResponseGetUsersDto>> GetMemberships(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _userService.GetUsersAsync(pageIndex, pageSize, textSearch, status, Constants.RoleUserName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership/{id}")]
        public async Task<ActionResult<ResponseGetUserDto>> GetMembership(Guid id)
        {
            var response = await _userService.GetUserAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("membership/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateMembershipTier(RequestUpdateMembershipTierOfMemberDto request, Guid id)
        {
            var response = await _userService.UpdateMembershipTierAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership-tier")]
        public async Task<ActionResult<ResponseGetMembershipTiersDto>> GetMembershipTier(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _membershipTierService.GetMembershipTiersAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership-tier/{id}")]
        public async Task<ActionResult<ResponseGetMembershipTierDto>> GetMembershipTier(Guid id)
        {
            var response = await _membershipTierService.GetMembershipTierAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("membership-tier")]
        public async Task<ActionResult<BaseResponseDto>> UploadMembershipTier(RequestUploadMembershipTierDto request)
        {
            var response = await _membershipTierService.UploadMembershipTierAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("membership-tier/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateMembershipTier(RequestUpdateMembershipTierDto request, Guid id)
        {
            var response = await _membershipTierService.UpdateMembershipTierAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("membership-tier/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteMembershipTier(Guid id)
        {
            var response = await _membershipTierService.SoftDeleteMembershipTierAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership-tier-benefit")]
        public async Task<ActionResult<ResponseGetMembershipTierBenefitsDto>> GetMembershipTierBenefits(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _membershipTierBenefitService.GetMembershipTierBenefitsAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("membership-tier-benefit/{id}")]
        public async Task<ActionResult<ResponseGetMembershipTierBenefitDto>> GetMembershipTierBenefit(Guid id)
        {
            var response = await _membershipTierBenefitService.GetMembershipTierBenefitAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("membership-tier-benefit")]
        public async Task<ActionResult<BaseResponseDto>> UploadMembershipTierBenefit(RequestUploadMembershipTierBenefitDto request)
        {
            var response = await _membershipTierBenefitService.UploadMembershipTierBenefitAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("membership-tier-benefit/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateMembershipTierBenefit(RequestUpdateMembershipTierBenefitDto request, Guid id)
        {
            var response = await _membershipTierBenefitService.UpdateMembershipTierBenefitAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("membership-tier-benefit/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteMembershipTierBenefit(Guid id)
        {
            var response = await _membershipTierBenefitService.SoftDeleteMembershipTierBenefitAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("admin-account")]
        public async Task<ActionResult<ResponseGetUsersDto>> GetAdminAccounts(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _userService.GetUsersAsync(pageIndex, pageSize, textSearch, status, Constants.RoleAdminName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("admin-account/{id}")]
        public async Task<ActionResult<ResponseGetUserDto>> GetAdminAccount(Guid id)
        {
            var response = await _userService.GetUserAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("admin-account/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateAdminAccount(RequestUpdateAdminAccountDto request, Guid id)
        {
            var response = await _userService.UpdateAdminAccount(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("admin-account")]
        public async Task<ActionResult<BaseResponseDto>> UploadAdminAccount(RequestUploadAdminAccountDto request)
        {
            var response = await _userService.UploadAdminAccount(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("admin-account/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteAdminAccount(Guid id)
        {
            var response = await _userService.SoftDeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("room")]
        public async Task<ActionResult<ResponseGetRoomsDto>> GetRooms(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? branchId, Guid? roomCategoryId)
        {
            var response = await _roomService.GetRoomsAsync(pageIndex, pageSize, textSearch, status, branchId, roomCategoryId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("room/{id}")]
        public async Task<ActionResult<ResponseGetRoomDto>> GetRoom(Guid id)
        {
            var response = await _roomService.GetRoomAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("room")]
        public async Task<ActionResult<BaseResponseDto>> UploadRoom(RequestUploadRoomDto request)
        {
            var response = await _roomService.UploadRoomAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("room/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateRoom(RequestUpdateRoomDto request, Guid id)
        {
            var response = await _roomService.UpdateRoomAsync(request, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("room/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteRoom(Guid id)
        {
            var response = await _roomService.SoftDeleteRoomAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
