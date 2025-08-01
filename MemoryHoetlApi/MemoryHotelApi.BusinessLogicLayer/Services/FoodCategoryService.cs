using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class FoodCategoryService : GenericService<FoodCategory>, IFoodCategoryService
    {
        public FoodCategoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<ResponseAdminGetFoodCategoriesDto> GetFoodCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<FoodCategory>(x => !x.IsDeleted);

            // Check if textSearch is provided and filter accordingly
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Name != null && x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase));
            }
            // Check if status is provided and filter accordingly
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            var includes = new[]
            {
                nameof(FoodCategory.SubFoodCategories)
            };

            // Get all the food categories from the database
            var foodCategories = await _unitOfWork.FoodCategoryRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // Count the total records
            var totalRecords = await _unitOfWork.FoodCategoryRepository!.CountEntities(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the food categories to the response DTO
            var foodCategoryDtos = _mapper.Map<List<AdminFoodCategoryDto>>(foodCategories.OrderBy(x => x.Order));

            return new ResponseAdminGetFoodCategoriesDto
            {
                StatusCode = 200,
                Data = foodCategoryDtos,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };

        }

        public async Task<ResponseAdminGetFoodCategoryDto> GetFoodCategoryAsync(Guid id)
        {

            var includes = new string[]
            {
                nameof(FoodCategory.SubFoodCategories)
            };

            // Find the food category by ID
            var foodCategory = await _unitOfWork.FoodCategoryRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // If food category is not found or is deleted
            if (foodCategory == null)
            {
                return new ResponseAdminGetFoodCategoryDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food category not found or has been deleted."
                };
            }

            // Map the food category to the response DTO
            var foodCategoryDto = _mapper.Map<AdminFoodCategoryDto>(foodCategory);
            
            return new ResponseAdminGetFoodCategoryDto
            {
                StatusCode = 200,
                Data = foodCategoryDto,
                IsSuccess = true,
                Message = "Food category retrieved successfully."
            };

        }

        public async Task<BaseResponseDto> SoftDeleteFoodCategoryAsync(Guid id)
        {
            // Find the food category by ID
            var foodCategory = await _unitOfWork.FoodCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted);

            // If food category is not found or is already deleted
            if (foodCategory == null || foodCategory.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food category not found or has already been deleted."
                };
            }

            // Mark the food category as deleted
            foodCategory.IsDeleted = true;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food category deleted successfully."
            };

        }

        public async Task<BaseResponseDto> UpdateFoodCategoryAsync(Guid id, RequestUpdateFoodCategoryDto request)
        {
            // Find the food category by ID
            var foodCategory = await _unitOfWork.FoodCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted);

            // If food category is not found or is already deleted
            if (foodCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food category not found or has already been deleted."
                };
            }

            // Update the food category properties
            foodCategory.Name = request.Name ?? foodCategory.Name;
            foodCategory.IsActive = request.IsActive ?? foodCategory.IsActive;
            foodCategory.Order = request.Order ?? foodCategory.Order;
            foodCategory.Description = request.Description ?? foodCategory.Description;
            foodCategory.Icon = request.Icon ?? foodCategory.Icon;

            // Save changes to the database
            _unitOfWork.FoodCategoryRepository.Update(foodCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food category updated successfully."
            };
        }

        public async Task<BaseResponseDto> UploadFoodCategoryAsync(RequestUploadFoodCategoryDto request)
        {
            // Get all food categories that are not deleted
            var maxOrder = await _unitOfWork.FoodCategoryRepository!.GetMaxOrder();

            // Check if the Order is null or not
            if (!request.Order.HasValue || request.Order == null || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping the request to the FoodCategory entity
            var foodCategory = _mapper.Map<FoodCategory>(request);

            // Add the new food category to the database
            await _unitOfWork.FoodCategoryRepository!.AddAsync(foodCategory);
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food category uploaded successfully."
            };
        }
    }
}
