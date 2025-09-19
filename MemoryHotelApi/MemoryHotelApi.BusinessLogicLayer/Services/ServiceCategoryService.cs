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
    public class ServiceCategoryService : GenericService<ServiceCategory>, IServiceCategoryService
    {
        private readonly StringUtility _stringUtility;

        public ServiceCategoryService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseAdminGetServiceCategoriesDto> GetServiceCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<ServiceCategory>(x => !x.IsDeleted);

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

            // Get all the service categories from the database
            var serviceCategories = await _unitOfWork.ServiceCategoryRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total records
            var totalRecords = await _unitOfWork.ServiceCategoryRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the service categories to the response DTO
            var serviceCategoriesDto = _mapper.Map<List<AdminServiceCategoryDto>>(serviceCategories.OrderBy(x => x.Order));

            return new ResponseAdminGetServiceCategoriesDto
            {
                StatusCode = 200,
                Data = serviceCategoriesDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };
        }

        public async Task<ResponseGetServiceCategoriesExploreDto> GetServiceCategoriesExploreAsync(int? pageIndex, int? pageSize, string? textSearch)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<ServiceCategory>(x => !x.IsDeleted && x.IsActive);

            // Check if textSearch is provided and filter accordingly
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Name != null && x.Name.Contains(textSearch));
            }

            // Get all the service categories from the database
            var serviceCategories = await _unitOfWork.ServiceCategoryRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total records
            var totalRecords = await _unitOfWork.ServiceCategoryRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the service categories to the response DTO
            var serviceCategoriesDto = _mapper.Map<List<ServiceCategoryExploreDto>>(serviceCategories.OrderBy(x => x.Order));

            return new ResponseGetServiceCategoriesExploreDto
            {
                StatusCode = 200,
                Data = serviceCategoriesDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };
        }

        public async Task<ResponseAdminGetServiceCategoryDto> GetServiceCategoryAsync(Guid id)
        {
            // Find the service category by ID
            var serviceCategory = await _unitOfWork.ServiceCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted);

            // If service category is not found or is deleted
            if (serviceCategory == null)
            {
                return new ResponseAdminGetServiceCategoryDto
                {
                    StatusCode = 404,
                    Message = "Service category not found or has been deleted."
                };
            }

            // Map the service category to the response DTO
            var serviceCategoryDto = _mapper.Map<AdminServiceCategoryDto>(serviceCategory);

            return new ResponseAdminGetServiceCategoryDto
            {
                StatusCode = 200,
                Data = serviceCategoryDto,
                Message = "Service category retrieved successfully."
            };
        }

        public async Task<ResponseGetServiceCategoryExploreDto> GetServiceCategoryExploreAsync(Guid id)
        {
            // Find the service category by ID
            var serviceCategory = await _unitOfWork.ServiceCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted && x.IsActive);

            // If service category is not found or is deleted
            if (serviceCategory == null)
            {
                return new ResponseGetServiceCategoryExploreDto
                {
                    StatusCode = 404,
                    Message = "Service category not found or has been deleted."
                };
            }

            // Map the service category to the response DTO
            var serviceCategoryDto = _mapper.Map<ServiceCategoryExploreDto>(serviceCategory);

            return new ResponseGetServiceCategoryExploreDto
            {
                StatusCode = 200,
                Data = serviceCategoryDto,
                Message = "Service category retrieved successfully."
            };
        }

        public async Task<BaseResponseDto> SoftDeleteServiceCategoryAsync(Guid id)
        {
            // Find the service category by ID
            var serviceCategory = await _unitOfWork.ServiceCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted);

            // If service category is not found or is already deleted
            if (serviceCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Service category not found or has already been deleted."
                };
            }

            // Mark the service category as deleted
            serviceCategory.IsDeleted = true;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Service category deleted successfully."
            };
        }

        public async Task<BaseResponseDto> UpdateServiceCategoryAsync(Guid id, RequestUpdateServiceCategoryDto request)
        {
            // Find the service category by ID
            var serviceCategory = await _unitOfWork.ServiceCategoryRepository!.GetByIdAsync(id, predicate: x => !x.IsDeleted);

            // If service category is not found or is already deleted
            if (serviceCategory == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Service category not found or has already been deleted."
                };
            }

            // If the name is provided, format it and update the service category name
            if (!string.IsNullOrEmpty(request.Name))
            {
                request.Name = _stringUtility.FomartStringName(request.Name);

                // Check if the name already exists in the database
                var existingServiceCategory = await _unitOfWork.ServiceCategoryRepository!.GetEntityWithConditionAsync(
                    x => x.Name.Equals(request.Name) && x.Id != id && !x.IsDeleted);

                // If the name already exists, return an error response
                if (existingServiceCategory != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Service category name already exists."
                    };
                }

                // Update the service category name
                serviceCategory.Name = request.Name;
            }

            // Update the service category properties
            serviceCategory.IsActive = request.IsActive ?? serviceCategory.IsActive;
            serviceCategory.Order = request.Order ?? serviceCategory.Order;
            serviceCategory.Description = request.Description ?? serviceCategory.Description;
            serviceCategory.Icon = request.Icon ?? serviceCategory.Icon;

            // Save changes to the database
            _unitOfWork.ServiceCategoryRepository.Update(serviceCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Service category updated successfully."
            };
        }

        public async Task<BaseResponseDto> UploadServiceCategoryAsync(RequestUploadServiceCategoryDto request)
        {
            // Format the name of the service category
            request.Name = _stringUtility.FomartStringName(request.Name);

            // Check if the name is existing in the database
            var existingServiceCategory = await _unitOfWork.ServiceCategoryRepository!.GetEntityWithConditionAsync(
                x => x.Name.Equals(request.Name) && !x.IsDeleted);

            // If the name already exists, return an error response
            if (existingServiceCategory != null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Service category name already exists."
                };
            }

            // Get all service categories that are not deleted
            var maxOrder = await _unitOfWork.ServiceCategoryRepository!.GetMaxOrderAsync();

            // Check if the Order is null or not
            if (!request.Order.HasValue || request.Order == null || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping the request to the serviceCategory entity
            var serviceCategory = _mapper.Map<ServiceCategory>(request);

            // Add the new service category to the database
            await _unitOfWork.ServiceCategoryRepository!.AddAsync(serviceCategory);
            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Service category uploaded successfully."
            };
        }
    }
}
