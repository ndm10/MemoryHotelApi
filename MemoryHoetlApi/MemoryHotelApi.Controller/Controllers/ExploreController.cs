using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExploreController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly IBranchService _branchService;

        public ExploreController(IBannerService bannerService, IBranchService branchService)
        {
            _bannerService = bannerService;
            _branchService = branchService;
        }

        [HttpGet("banner")]
        public async Task<ActionResult<ResponseGetBannersExploreDto>> GetBanners()
        {
            var response = await _bannerService.GetAllBannersAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("branch")]
        public async Task<ActionResult<ResponseGetBranchesExploreDto>> GetBranches()
        {
            var response = await _branchService.GetBranchesExploreAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("branch/{id}")]
        public async Task<ActionResult<ResponseGetBranchExploreDto>> GetBranch(Guid id)
        {
            var response = await _branchService.GetBranchExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
