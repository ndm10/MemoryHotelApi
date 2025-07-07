using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
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
        private readonly ITourService _tourService;
        private readonly ICityService _cityService;
        private readonly IStoryService _storyService;
        private readonly IBlogWriterService _blogWriterService;

        public ExploreController(IBannerService bannerService, IBranchService branchService, ITourService tourService, ICityService cityService, IStoryService storyService, IBlogWriterService blogWriterService)
        {
            _bannerService = bannerService;
            _branchService = branchService;
            _tourService = tourService;
            _cityService = cityService;
            _storyService = storyService;
            _blogWriterService = blogWriterService;
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

        [HttpGet("city")]
        public async Task<ActionResult<ResponseGetCitiesExploreDto>> GetCities()
        {
            var response = await _cityService.GetCitiesExploreAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("tour/{id}")]
        public async Task<ActionResult<ResponseGetTourExploreDto>> GetTour(Guid id)
        {
            var response = await _tourService.GetTourExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("story")]
        public async Task<ActionResult<ResponseGetStoriesExploreDto>> GetStories()
        {
            var response = await _storyService.GetStoriesExploreAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("blog")]
        public async Task<ActionResult<GenericResponsePagination<BlogExploreDto>>> GetBlogs(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var response = await _blogWriterService.GetBlogsExploreAsync(pageIndex, pageSize, textSearch, status);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("blog/{id}")]
        public async Task<ActionResult<ResponseGetBlogExploreDto>> GetBlog(Guid id)
        {
            var response = await _blogWriterService.GetBlogExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
