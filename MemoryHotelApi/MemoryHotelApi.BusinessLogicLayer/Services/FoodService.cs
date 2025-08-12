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
using System;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class FoodService : GenericService<Food>, IFoodService
    {
        private readonly StringUtility _stringUtility;

        public FoodService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseAdminGetFoodDto> GetFoodAsync(Guid id)
        {
            // Include the subFoodCategory
            var includes = new[]
            {
                nameof(Food.SubFoodCategory),
            };

            // Find the food by id and map it to ResponseAdminGetFoodDto
            var food = await _unitOfWork.FoodRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // If food is not found, return not found response
            if (food == null)
            {
                return new ResponseAdminGetFoodDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food not found or has been deleted."
                };
            }

            return new ResponseAdminGetFoodDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food retrieved successfully.",
                Data = _mapper.Map<AdminFoodDto>(food)
            };

        }

        public async Task<ResponseGetFoodExploreDto> GetFoodExploreAsync(Guid id)
        {
            // Find the food by id and map it to ResponseGetFoodExploreDto
            var food = await _unitOfWork.FoodRepository!.GetByIdAsync(id, null, x => !x.IsDeleted && x.IsActive);

            // If food is not found, return not found response
            if (food == null)
            {
                return new ResponseGetFoodExploreDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food not found or has been deleted."
                };
            }

            return new ResponseGetFoodExploreDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food retrieved successfully.",
                Data = _mapper.Map<ExploreFoodDto>(food)
            };
        }

        public async Task<ResponseAdminGetFoodsDto> GetFoodsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? subFoodCategoryId)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a query to get foods with optional filters
            var predicate = PredicateBuilder.New<Food>(x => !x.IsDeleted);

            // Apply text search if provided
            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                predicate = predicate.And(x => x.Name.Contains(textSearch) || (x.Description != null && x.Description.Contains(textSearch)));
            }

            // Apply status filter if provided
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status.Value);
            }

            // Apply subFoodCategoryId filter if provided
            if (subFoodCategoryId.HasValue)
            {
                predicate = predicate.And(x => x.SubFoodCategoryId == subFoodCategoryId.Value);
            }

            // Include the subFoodCategory
            var includes = new[]
            {
                nameof(Food.SubFoodCategory),
            };

            // Get all the food categories from the database
            var foods = await _unitOfWork.FoodRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // Count the total records
            var totalRecords = await _unitOfWork.FoodRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the food categories to the response DTO
            var foodDtos = _mapper.Map<List<AdminFoodDto>>(foods.OrderBy(x => x.Order));

            return new ResponseAdminGetFoodsDto
            {
                StatusCode = 200,
                Data = foodDtos,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };

        }

        public async Task<ResponseGetFoodsExploreDto> GetFoodsExploreAsync(int? pageIndex, int? pageSize, string? textSearch, Guid? foodCategoryId, Guid? subFoodCategoryId)
        {
            // Create the predicate for filtering
            var predicate = PredicateBuilder.New<Food>(x => !x.IsDeleted && x.IsActive);

            // Apply text search if provided
            if (!string.IsNullOrWhiteSpace(textSearch))
            {
                predicate = predicate.And(x => x.Name.Contains(textSearch) || (x.Description != null && x.Description.Contains(textSearch)));
            }

            // Apply foodCategoryId filter if provided
            if (foodCategoryId.HasValue)
            {
                predicate = predicate.And(x => x.SubFoodCategory.FoodCategoryId == foodCategoryId.Value);

                // If subFoodCategoryId is provided, continue filter by subFoodCategoryId
                if (subFoodCategoryId.HasValue)
                {
                    // Check if the subFoodCategoryId belong to the foodCategoryId, then apply the filter
                    var isSubFoodCategoryBelongToFoodCategory = await _unitOfWork.FoodCategoryRepository!.IsIncludeSubFoodCategoryAsync(foodCategoryId.Value, subFoodCategoryId.Value);

                    // If the subFoodCategoryId does not belong to the foodCategoryId, return conflict response
                    if (!isSubFoodCategoryBelongToFoodCategory)
                    {
                        return new ResponseGetFoodsExploreDto
                        {
                            StatusCode = 409,
                            Message = "SubFoodCategory does not belong to the FoodCategory."
                        };
                    }

                    predicate = predicate.And(x => x.SubFoodCategoryId == subFoodCategoryId.Value);
                }
            }
            else
            {
                // If foodCategoryId is not provided, check if subFoodCategoryId is provided
                if (subFoodCategoryId.HasValue)
                {
                    predicate = predicate.And(x => x.SubFoodCategoryId == subFoodCategoryId.Value);
                }
            }

            var foods = new List<Food>();
            var totalRecords = 0;
            var totalPages = 0;

            // Check if pageIndex and pageSize are provided
            // If not, get all foods, other wise apply pagination
            if (pageIndex.HasValue || pageSize.HasValue)
            {
                // If pageIndex or pageSize is not provided, set it to default values
                pageIndex ??= Constants.PageIndexDefault;
                pageSize ??= Constants.PageSizeDefault;

                // Get the foods with pagination from the database
                foods = await _unitOfWork.FoodRepository!.GenericGetPaginationAsync(pageIndex.Value, pageSize.Value, predicate, null);

                // Calculate the total records and total pages
                totalRecords = await _unitOfWork.FoodRepository!.CountEntitiesAsync(predicate);
                totalPages = (int)Math.Ceiling((decimal)totalRecords / Constants.PageSizeDefault);
            }
            else
            {
                // Get all the foods from the database
                foods = (await _unitOfWork.FoodRepository!.GetAllAsync(predicate)).ToList();
            }

            // Map the foods to the response DTO
            var foodsDto = _mapper.Map<List<ExploreFoodDto>>(foods.OrderBy(x => x.Order));

            return new ResponseGetFoodsExploreDto
            {
                StatusCode = 200,
                Message = "Foods retrieved successfully.",
                Data = foodsDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<BaseResponseDto> SoftDeleteFoodAsync(Guid id)
        {
            // Find the food by id
            var food = await _unitOfWork.FoodRepository!.GetByIdAsync(id, null, x => !x.IsDeleted);

            // If food is not found, return not found response
            if (food == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food not found or has been deleted."
                };
            }

            // Soft delete the food
            food.IsDeleted = true;
            _unitOfWork.FoodRepository.Update(food);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food deleted successfully."
            };
        }

        public async Task<BaseResponseDto> UpdateFoodAsync(Guid id, RequestUpdateFoodDto request)
        {
            // Include the subFoodCategory
            var includes = new[]
            {
                nameof(Food.SubFoodCategory),
            };

            // Find the food by id
            var food = await _unitOfWork.FoodRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // If food is not found, return not found response
            if (food == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Food not found or has been deleted."
                };
            }

            // Check if the subFoodCategoryId has provided and is exists in the database
            if (request.SubFoodCategoryId != Guid.Empty && request.SubFoodCategoryId.HasValue)
            {
                var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetByIdAsync(request.SubFoodCategoryId.Value, null, x => !x.IsDeleted);

                if (subFoodCategory == null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "SubFoodCategory not found or has been deleted."
                    };
                }

                // Update the subFoodCategoryId
                food.SubFoodCategoryId = request.SubFoodCategoryId.Value;
                food.SubFoodCategory = subFoodCategory;
            }

            // Format the name of the food and check if it already exists except for the current food
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = _stringUtility.FomartStringName(request.Name);

                // Check if the food with the same name already exists
                var existingFood = await _unitOfWork.FoodRepository!.GetEntityWithConditionAsync(
                    x => x.Name == request.Name && !x.IsDeleted && x.Id != id);

                // If food already exists, return conflict response
                if (existingFood != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 409,
                        IsSuccess = false,
                        Message = "Food with the same name already exists."
                    };
                }

                // Update the name of the food
                food.Name = request.Name;
            }

            // Map the request to the food entity
            food.Price = request.Price ?? food.Price;
            food.Image = request.Image ?? food.Image;
            food.IsBestSeller = request.IsBestSeller ?? food.IsBestSeller;
            food.WaitingTimeInMinute = request.WaitingTimeInMinute ?? food.WaitingTimeInMinute;
            food.Description = request.Description ?? food.Description;
            food.IsActive = request.IsActive ?? food.IsActive;
            food.Order = request.Order ?? food.Order;

            // Update the food entity
            _unitOfWork.FoodRepository.Update(food);
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Food updated successfully."
            };
        }

        public async Task<BaseResponseDto> UploadFoodAsync(RequestUploadFoodDto request)
        {
            // Format the name of the food and check if it already exists
            request.Name = _stringUtility.FomartStringName(request.Name);

            // Find the food by name
            var existingFood = await _unitOfWork.FoodRepository!.GetEntityWithConditionAsync(
                x => x.Name == request.Name && !x.IsDeleted);

            // If food already exists, return conflict response
            if (existingFood != null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 409,
                    IsSuccess = false,
                    Message = "Food already exists."
                };
            }

            // Check if the subFoodCategoryId is exists in the database
            var subFoodCategory = await _unitOfWork.SubFoodCategoryRepository!.GetByIdAsync(request.SubFoodCategoryId, null, x => !x.IsDeleted);

            if (subFoodCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "SubFoodCategory not found or has been deleted."
                };
            }

            // check if the order is provided, if not set it to maxOrder + 1
            if (request.Order == null || request.Order <= 0)
            {
                // Get the maximum order value
                var maxOrder = await _unitOfWork.FoodRepository!.GetMaxOrderAsync();
                request.Order = maxOrder + 1;
            }

            // Map the request to the food entity
            var food = _mapper.Map<Food>(request);
            food.SubFoodCategory = subFoodCategory;

            // Add the food entity to the repository
            await _unitOfWork.FoodRepository.AddAsync(food);
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = "Food uploaded successfully."
            };
        }
    }
}
