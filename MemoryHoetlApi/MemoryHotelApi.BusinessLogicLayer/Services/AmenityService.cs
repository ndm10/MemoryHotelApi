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
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AmenityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetAmenitiesDto> GetAmenities(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<Amenity>(x => !x.IsDeleted);

            // check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Name != null && x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the amenities from the database
            var amenities = await _unitOfWork.AmenityRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)amenities.Count() / pageSizeValue);

            return new ResponseGetAmenitiesDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<GetAmenityDto>>(amenities.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = amenities.Count()
            };
        }

        public async Task<ResponseGetAmenityDto> GetAmenity(Guid id)
        {
            // Find the amenity by id
            var amenity = await _unitOfWork.AmenityRepository!.GetByIdAsync(id);

            // Check if the amenity exists
            if (amenity == null || amenity.IsDeleted)
            {
                return new ResponseGetAmenityDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                };
            }

            // Map the amenity to the response DTO
            var amenityDto = _mapper.Map<GetAmenityDto>(amenity);

            return new ResponseGetAmenityDto
            {
                StatusCode = 200,
                Data = amenityDto,
                IsSuccess = true,
            };
        }

        public Task<BaseResponseDto> SoftDeleteAmenityAsync(Guid id)
        {
            // Find the amenity by id
            var amenity = _unitOfWork.AmenityRepository!.GetByIdAsync(id).Result;
            // Check if the amenity exists
            if (amenity == null || amenity.IsDeleted)
            {
                return Task.FromResult(new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                });
            }

            // Soft delete the amenity
            amenity.IsDeleted = true;
            _unitOfWork.AmenityRepository.Update(amenity);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa dịch vụ thành công.",
            });
        }

        public Task<BaseResponseDto> UpdateAmenityAsync(RequestUpdateAmenityDto request, Guid id)
        {
            // Find the amenity by id
            var amenity = _unitOfWork.AmenityRepository!.GetByIdAsync(id).Result;

            // Check if the amenity exists
            if (amenity == null || amenity.IsDeleted)
            {
                return Task.FromResult(new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                });
            }

            // Map the request to the amenity entity
            amenity.Name = request.Name?.Trim().Replace("\\s+", " ") ?? amenity.Name;
            amenity.Icon = request.Icon ?? amenity.Icon;
            amenity.Description = request.Description ?? amenity.Description;
            amenity.IsActive = request.IsActive ?? amenity.IsActive;
            amenity.Order = request.Order ?? amenity.Order;

            // Update the amenity in the database
            _unitOfWork.AmenityRepository.Update(amenity);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật dịch vụ thành công.",
            });
        }

        public async Task<BaseResponseDto> UploadAmenityAsync(RequestUploadAmenityDto request)
        {
            var predicate = PredicateBuilder.New<Amenity>(x => !x.IsDeleted && x.Name.Equals(request.Name.Trim().Replace("\\s+", " "), StringComparison.OrdinalIgnoreCase));

            var amenitiesExist = await _unitOfWork.AmenityRepository!.GetAllAsync(predicate);

            if (amenitiesExist != null && amenitiesExist.Count() > 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Dịch vụ đã tồn tại.",
                };
            }

            predicate = PredicateBuilder.New<Amenity>(x => !x.IsDeleted);
            var amenities = await _unitOfWork.AmenityRepository!.GetAllAsync(predicate);
            
            int order = 1;
            if (amenities != null && amenities.Count() > 0)
            {
                order = amenities.Max(x => x.Order) + 1;
            }

            var amenity = _mapper.Map<Amenity>(request);
            amenity.Order = order;
            amenity.IsActive = true;

            await _unitOfWork.AmenityRepository.AddAsync(amenity);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm dịch vụ thành công.",
            };
        }
    }
}
