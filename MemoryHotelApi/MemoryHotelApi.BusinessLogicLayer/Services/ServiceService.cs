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
    public class ServiceService : GenericService<Service>, IServiceService
    {
        private readonly StringUtility _stringUtility;
        public ServiceService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseAdminGetServiceDto> GetServiceAsync(Guid id)
        {
            // Include related entities
            var includes = new[]
            {
                nameof(Service.ServiceCategory)
            };

            // Find the service by id
            var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // Check if the service exists
            if (service == null)
            {
                return new ResponseAdminGetServiceDto
                {
                    StatusCode = 404,
                    Message = "Service not found!"
                };
            }

            // Map the service to the response DTO
            var serviceDto = _mapper.Map<AdminServiceDto>(service);

            return new ResponseAdminGetServiceDto
            {
                StatusCode = 200,
                Data = serviceDto,
                Message = "Get service successfully!"
            };
        }

        public async Task<ResponseGetServiceExploreDto> GetServiceExploreAsync(Guid id)
        {
            // Include related entities
            var includes = new[]
            {
                nameof(Service.ServiceCategory)
            };

            // Find the service by id
            var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted && x.IsActive);

            // Check if the service exists
            if (service == null)
            {
                return new ResponseGetServiceExploreDto
                {
                    StatusCode = 404,
                    Message = "Service not found!"
                };
            }

            // Map the service to the response DTO
            var serviceDto = _mapper.Map<ServiceExploreDto>(service);

            return new ResponseGetServiceExploreDto
            {
                StatusCode = 200,
                Data = serviceDto,
                Message = "Get service successfully!"
            };
        }

        public async Task<ResponseAdminGetServicesDto> GetServicesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? serviceCategoryId)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<Service>(x => !x.IsDeleted);

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
            if (serviceCategoryId.HasValue)
            {
                predicate = predicate.And(x => x.ServiceCategoryId == serviceCategoryId);
            }

            var includes = new[]
            {
                nameof(Service.ServiceCategory)
            };

            var orderBy = new Func<IQueryable<Service>, IOrderedQueryable<Service>>(q => q.OrderBy(x => x.Order));

            // Get all the service categories from the database
            var serviceCategories = await _unitOfWork.ServiceRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes, orderBy);

            // Count the total records
            var totalRecords = await _unitOfWork.ServiceRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the service categories to the response DTO
            var serviceCategoriesDto = _mapper.Map<List<AdminServiceDto>>(serviceCategories);

            return new ResponseAdminGetServicesDto
            {
                StatusCode = 200,
                Data = serviceCategoriesDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };
        }

        public async Task<ResponseGetServicesExploreDto> GetServicesExploreAsync(int? pageIndex, int? pageSize, string? textSearch, Guid? serviceCategoryId)
        {
            // Initialize default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<Service>(x => !x.IsDeleted && x.IsActive);

            // Check if textSearch is provided and filter accordingly
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Name != null && x.Name.Contains(textSearch));
            }
            if (serviceCategoryId.HasValue)
            {
                predicate = predicate.And(x => x.ServiceCategoryId == serviceCategoryId);
            }

            // Include related entities
            var includes = new[]
            {
                nameof(Service.ServiceCategory)
            };

            var orderBy = new Func<IQueryable<Service>, IOrderedQueryable<Service>>(q => q.OrderBy(x => x.Order));

            // Get all the service categories from the database
            var services = await _unitOfWork.ServiceRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes, orderBy);

            // Count the total records
            var totalRecords = await _unitOfWork.ServiceRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the service categories to the response DTO
            var servicesDto = _mapper.Map<List<ServiceExploreDto>>(services);

            // Return the response DTO
            return new ResponseGetServicesExploreDto
            {
                StatusCode = 200,
                Data = servicesDto,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };
        }

        public async Task<BaseResponseDto> SoftDeleteServiceAsync(Guid id)
        {
            // Find the service by id
            var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(id, null, x => !x.IsDeleted);

            // Check if the service exists
            if (service == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Service not found!"
                };
            }

            // Soft delete the service
            service.IsDeleted = true;

            // Update the service in the database
            _unitOfWork.ServiceRepository.Update(service);

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Service deleted successfully!"
            };
        }

        public async Task<BaseResponseDto> UpdateServiceAsync(Guid id, RequestUpdateServiceDto dto)
        {
            // Include related entities
            var includes = new[]
            {
                nameof(Service.ServiceCategory)
            };

            // Find the service by id
            var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(id, includes, x => !x.IsDeleted);

            // Check if the service exists
            if (service == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Service not found!"
                };
            }

            // Update the service
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                dto.Name = _stringUtility.FomartStringName(dto.Name);

                // Check if the food with the same name already exists
                var existingFood = await _unitOfWork.FoodRepository!.GetEntityWithConditionAsync(
                    x => x.Name == dto.Name && !x.IsDeleted && x.Id != id);

                // If food already exists, return conflict response
                if (existingFood != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 409,
                        IsSuccess = false,
                        Message = "Service with the same name already exists."
                    };
                }

                // Update the name of the food
                service.Name = dto.Name;

            }
            service.Price = dto.Price ?? service.Price;
            service.Description = dto.Description ?? service.Description;
            service.IsActive = dto.IsActive ?? service.IsActive;
            service.Image = dto.Image ?? service.Image;
            service.ServiceCategoryId = dto.ServiceCategoryId ?? service.ServiceCategoryId;

            // Save change to database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Service updated successfully!"
            };
        }

        public async Task<BaseResponseDto> UploadServiceAsync(RequestUploadServiceDto dto)
        {
            // Format the name of the service and check if it already exists
            dto.Name = _stringUtility.FomartStringName(dto.Name);

            // Find the service by name
            var existingService = await _unitOfWork.ServiceRepository!.GetEntityWithConditionAsync(
                x => x.Name == dto.Name && !x.IsDeleted);

            // If the service already exists, return conflict response
            if (existingService != null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 409,
                    IsSuccess = false,
                    Message = "Service with the same name already exists."
                };
            }

            // check if the order is provided, if not set it to maxOrder + 1
            if (dto.Order == null || dto.Order <= 0)
            {
                // Get the maximum order value
                var maxOrder = await _unitOfWork.ServiceRepository!.GetMaxOrderAsync();
                dto.Order = maxOrder + 1;
            }

            // Map the dto to the service entity
            var service = _mapper.Map<Service>(dto);

            // Add the service to the database
            await _unitOfWork.ServiceRepository.AddAsync(service);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = "Service uploaded successfully!"
            };
        }
    }
}
