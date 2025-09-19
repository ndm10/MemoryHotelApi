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
        private readonly IFoodCategoryService _foodCategoryService;
        private readonly ISubFoodCategoryService _subFoodCategoryService;
        private readonly IFoodService _foodService;
        private readonly IServiceCategoryService _serviceCategoryService;
        private readonly IServiceService _serviceService;

        public ExploreController(IBannerService bannerService, IBranchService branchService, ITourService tourService, ICityService cityService, IStoryService storyService, IBlogWriterService blogWriterService, IFoodCategoryService foodCategoryService, ISubFoodCategoryService subFoodCategoryService, IFoodService foodService, IServiceCategoryService serviceCategoryService, IServiceService serviceService)
        {
            _bannerService = bannerService;
            _branchService = branchService;
            _tourService = tourService;
            _cityService = cityService;
            _storyService = storyService;
            _blogWriterService = blogWriterService;
            _foodCategoryService = foodCategoryService;
            _subFoodCategoryService = subFoodCategoryService;
            _foodService = foodService;
            _serviceCategoryService = serviceCategoryService;
            _serviceService = serviceService;
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
        public async Task<ActionResult<GenericResponsePaginationDto<BlogExploreDto>>> GetBlogs(int? pageIndex, int? pageSize, string? textSearch, bool? status)
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

        [HttpGet("food-category")]
        public async Task<ActionResult<ResponseGetFoodCategoriesExploreDto>> GetFoodCategories()
        {
            var response = await _foodCategoryService.GetFoodCategoriesExploreAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("food-category/{id}")]
        public async Task<ActionResult<ResponseGetSubFoodCategoriesExploreDto>> GetSubFoodCategories(Guid id)
        {
            var response = await _foodCategoryService.GetFoodCategoryExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("sub-food-category")]
        public async Task<ActionResult<ResponseGetSubFoodCategoriesExploreDto>> GetSubFoodCategories(Guid? foodCategoryId)
        {
            var response = await _subFoodCategoryService.GetSubFoodCategoriesExploreAsync(foodCategoryId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("sub-food-category/{id}")]
        public async Task<ActionResult<ResponseGetSubFoodCategoryExploreDto>> GetFoodsBySubFoodCategory(Guid id)
        {
            var response = await _subFoodCategoryService.GetSubFoodCategoryExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("food")]
        public async Task<ActionResult<ResponseGetFoodsExploreDto>> GetFoods(int? pageIndex, int? pageSize, string? textSearch, Guid? foodCategoryId, Guid? subFoodCategoryId)
        {
            var response = await _foodService.GetFoodsExploreAsync(pageIndex, pageSize, textSearch, foodCategoryId, subFoodCategoryId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("food/{id}")]
        public async Task<ActionResult<ResponseGetFoodExploreDto>> GetFood(Guid id)
        {
            var response = await _foodService.GetFoodExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("service-category")]
        public async Task<ActionResult<ResponseGetServiceCategoriesExploreDto>> GetServiceCategories(int? pageIndex, int? pageSize, string? textSearch)
        {
            var response = await _serviceCategoryService.GetServiceCategoriesExploreAsync(pageIndex, pageSize, textSearch);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("service-category/{id}")]
        public async Task<ActionResult<ResponseGetServiceCategoriesExploreDto>> GetServiceCategories(Guid id)
        {
            var response = await _serviceCategoryService.GetServiceCategoryExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("service")]
        public async Task<ActionResult<ResponseGetServicesExploreDto>> GetServices(int? pageIndex, int? pageSize, string? textSearch, Guid? serviceCategoryId)
        {
            var response = await _serviceService.GetServicesExploreAsync(pageIndex, pageSize, textSearch, serviceCategoryId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("service/{id}")]
        public async Task<ActionResult<ResponseGetServiceExploreDto>> GetService(Guid id)
        {
            var response = await _serviceService.GetServiceExploreAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
