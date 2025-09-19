using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class SubFoodCategoryService : GenericService<SubFoodCategory>, ISubFoodCategoryService
    {
        private readonly StringUtility _stringUtility;

        public SubFoodCategoryService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseAdminGetSubFoodCategoriesDto> GetSubFoodCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? foodCategoryId)
        {
            // Initialize the default value of pageIndex and pageSize
            pageIndex ??= Constants.PageIndexDefault;
            pageSize ??= Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<SubFoodCategory>(x => !x.IsDeleted);

            // Check if textSearch is provided and filter accordingly
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Name != null && x.Name.Contains(textSearch));
            }

            // Check if status is provided and filter accordingly
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Check if subFoodCategoryId is provided
            if(foodCategoryId.HasValue && foodCategoryId != Guid.Empty)
            {
                predicate = predicate.And(x => x.FoodCategoryId == foodCategoryId);
            }

            var includes = new[]
            {
                nameof(SubFoodCategory.FoodCategory)
            };

            // Get all the sub food categories from the database
            var subFoodCategories = await _unitOfWork.SubFoodCategoryRepository!.GenericGetPaginationAsync(pageIndex.Value, pageSize.Value, predicate, includes);

            // Count the total records
            var totalRecords = await _unitOfWork.SubFoodCategoryRepository!.CountEntitiesAsync(predicate);

            // Calculate the total pages
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize.Value);

            // Map the sub food categories to the response DTO
            var subFoodCategoriesDto = _mapper.Map<List<AdminSubFoodCategoryDto>>(subFoodCategories.OrderBy(x => x.Order));

            return new ResponseAdminGetSubFoodCategoriesDto
            {
                StatusCode = 200,
                Data = subFoodCategoriesDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };

        }

        public async Task<ResponseGetSubFoodCategoriesExploreDto> GetSubFoodCategoriesExploreAsync(Guid? foodCategoryId)
        {
            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<SubFoodCategory>(x => !x.IsDeleted && x.IsActive);

            // Check if foodCategoryId is provided and filter accordingly
            if (foodCategoryId.HasValue && foodCategoryId != Guid.Empty)
            {
                predicate = predicate.And(x => x.FoodCategoryId == foodCategoryId);
            }

            // Include the food category and food
            var includes = new string[]
            {
                nameof(SubFoodCategory.FoodCategory),
                nameof(SubFoodCategory.Foods)
            };

            // Get all the sub food categories from the database
            var subFoodCategories = await _unitOfWork.SubFoodCategoryRepository!.GetAllAsync(predicate, includes);

            // Map the sub food categories to the response DTO
            var subFoodCategoriesDto = _mapper.Map<List<SubFoodCategoryExploreDto>>(subFoodCategories.OrderBy(x => x.Order));

            // Return the response DTO
            return new ResponseGetSubFoodCategoriesExploreDto
            {
                StatusCode = 200,
                Data = subFoodCategoriesDto,
            };
        }

        public async Task<ResponseAdminGetSubFoodCategoryDto> GetSubFoodCategoryAsync(Guid id)
        {
            // Get the sub food category by id
            var includes = new string[]
            {
                nameof(SubFoodCategory.FoodCategory)
            };

            var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetEntityWithConditionAsync(x => x.Id == id && !x.IsDeleted, includes);

            if (subFoodCategory == null)
            {
                return new ResponseAdminGetSubFoodCategoryDto
                {
                    StatusCode = 404,
                    Message = "Sub food category not found."
                };
            }

            // Map the sub food category to the response DTO
            var subFoodCategoryDto = _mapper.Map<AdminSubFoodCategoryDto>(subFoodCategory);

            return new ResponseAdminGetSubFoodCategoryDto
            {
                StatusCode = 200,
                Data = subFoodCategoryDto
            };
        }

        public async Task<ResponseGetSubFoodCategoryExploreDto> GetSubFoodCategoryExploreAsync(Guid id)
        {
            // Include the food in the response
            var includes = new string[]
            {
                nameof(SubFoodCategory.Foods),
                nameof(SubFoodCategory.FoodCategory)
            };

            // Get the sub food category by id
            var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted && x.IsActive);

            // If sub food category is not found or is deleted
            if (subFoodCategory == null)
            {
                return new ResponseGetSubFoodCategoryExploreDto
                {
                    StatusCode = 404,
                    Message = "Sub food category not found or has been deleted."
                };
            }

            // Map the sub food category to the response DTO
            var subFoodCategoryDto = _mapper.Map<SubFoodCategoryExploreDto>(subFoodCategory);

            // Return the response DTO
            return new ResponseGetSubFoodCategoryExploreDto
            {
                StatusCode = 200,
                Data = subFoodCategoryDto
            };
        }

        public async Task<BaseResponseDto> SoftDeleteSubFoodCategoryAsync(Guid id)
        {
            // Include the food in the deletion
            var includes = new string[]
            {
                nameof(SubFoodCategory.Foods)
            };

            // Find the sub food category by ID
            var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // If sub food category is not found or is deleted
            if (subFoodCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Sub food category not found or has been deleted."
                };
            }

            // Check if the sub food category has any associated foods
            if (subFoodCategory.Foods != null && subFoodCategory.Foods.Any(x => !x.IsDeleted))
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Cannot delete sub food category with associated foods. Please delete the foods first."
                };
            }

            // Soft delete the sub food category
            subFoodCategory.IsDeleted = true;
            _unitOfWork.SubFoodCategoryRepository.Update(subFoodCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Sub food category deleted successfully."
            };
        }

        public async Task<BaseResponseDto> UpdateSubFoodCategoryAsync(Guid id, RequestUpdateSubFoodCategoryDto request)
        {
            // Include the food category in the update
            var includes = new string[]
            {
                nameof(SubFoodCategory.FoodCategory)
            };

            // Find the sub food category by ID
            var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);


            // If sub food category is not found or is deleted
            if (subFoodCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Sub food category not found or has been deleted."
                };
            }

            // If the name is provided, format it and update the sub food category name
            if (!string.IsNullOrEmpty(request.Name))
            {
                request.Name = _stringUtility.FomartStringName(request.Name);

                // Check if the name already exists in the database
                var existingSubFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetEntityWithConditionAsync(
                    x => x.Name.Equals(request.Name) && x.Id != id && !x.IsDeleted);

                // If the name already exists, return an error response
                if (existingSubFoodCategory != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Sub food category name already exists."
                    };
                }

                // Update the sub food category name
                subFoodCategory.Name = request.Name;
            }

            // Check if the food category ID is provided and update the sub food category's food category
            if (request.FoodCategoryId.HasValue && request.FoodCategoryId != Guid.Empty)
            {
                // Check if the food category exists
                var foodCategory = await _unitOfWork.FoodCategoryRepository!.GetByIdAsync(request.FoodCategoryId.Value, null, x => !x.IsDeleted);

                // If the food category does not exist, return an error response
                if (foodCategory == null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 404,
                        IsSuccess = false,
                        Message = "Food category not found."
                    };
                }

                // Update the sub food category's food category
                subFoodCategory.FoodCategoryId = request.FoodCategoryId.Value;
                subFoodCategory.FoodCategory = foodCategory;
            }

            // Update the sub food category properties
            subFoodCategory.IsActive = request.IsActive ?? subFoodCategory.IsActive;
            subFoodCategory.Order = request.Order ?? subFoodCategory.Order;
            subFoodCategory.Description = request.Description ?? subFoodCategory.Description;

            // Save changes to the database
            _unitOfWork.SubFoodCategoryRepository.Update(subFoodCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Sub food category updated successfully."
            };

        }

        public async Task<BaseResponseDto> UploadSubFoodCategoryAsync(RequestUploadSubFoodCategoryDto request)
        {
            // Check if the CategoryId is exists in the database
            var foodCategory = await _unitOfWork.FoodCategoryRepository!.GetByIdAsync(request.FoodCategoryId, null, x => !x.IsDeleted);

            if (foodCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food category not found."
                };
            }

            // Format the name
            request.Name = _stringUtility.FomartStringName(request.Name);

            // Check if the name is exists in the database
            var existingSubFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetEntityWithConditionAsync(
                x => x.Name.Equals(request.Name) && !x.IsDeleted);

            // If the name already exists, return an error response
            if (existingSubFoodCategory != null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Sub food category name already exists."
                };
            }

            // Get the max order value from the database
            var maxOrder = await _unitOfWork.SubFoodCategoryRepository!.GetMaxOrderAsync();

            // Check if the Order is null or not
            if (!request.Order.HasValue || request.Order == null || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                request.Order = maxOrder + 1;
            }

            // Create a new sub food category entity
            var subFoodCategory = _mapper.Map<SubFoodCategory>(request);
            subFoodCategory.FoodCategory = foodCategory;

            // Add the new sub food category to the database
            await _unitOfWork.SubFoodCategoryRepository.AddAsync(subFoodCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Sub food category created successfully."
            };
        }
    }
}
