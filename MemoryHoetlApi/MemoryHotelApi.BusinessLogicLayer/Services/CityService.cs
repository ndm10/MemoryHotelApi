using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using System;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetCitiesDto> GetCities(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<City>(x => !x.IsDeleted);

            // check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Name != null && x.Name.Contains(textSearch)) || (x.Region != null && x.Region.Contains(textSearch)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the cities from the database
            var cities = await _unitOfWork.CityRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total records
            var totalRecords = await _unitOfWork.CityRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            // Map the cities to the response DTO
            return new ResponseGetCitiesDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<GetCityDto>>(cities.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = totalRecords,
                IsSuccess = true,
            };

        }

        public async Task<ResponseGetCitiesExploreDto> GetCitiesExploreAsync()
        {
            // Get all the cities from the database
            var predicate = PredicateBuilder.New<City>(x => !x.IsDeleted && x.IsActive);

            var cities = await _unitOfWork.CityRepository!.GetAllCities(predicate);

            // Map the cities to the response DTO
            return new ResponseGetCitiesExploreDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<CityExploreDto>>(cities),
            };
        }

        public async Task<ResponseGetCityDto> GetCity(Guid id)
        {
            var city = await _unitOfWork.CityRepository!.GetByIdAsync(id);

            if (city == null || city.IsDeleted)
            {
                return new ResponseGetCityDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "City không tồn tại.",
                };
            }

            // Map the city to the response DTO
            var cityDto = _mapper.Map<GetCityDto>(city);
            return new ResponseGetCityDto
            {
                StatusCode = 200,
                Data = cityDto,
                IsSuccess = true,
            };
        }

        public Task<BaseResponseDto> SoftDeleteCityAsync(Guid id)
        {
            // Find the city by the ID
            var city = _unitOfWork.CityRepository!.GetByIdAsync(id).Result;

            // If the city not found
            if(city == null || city.IsDeleted)
            {
                return Task.FromResult(new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "City không tồn tại.",
                });
            }

            // Update the delete flag of the city
            city.IsDeleted = true;

            // Save the changes to the database
            _unitOfWork.CityRepository.Update(city);

            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa thành công.",
            });
        }

        public Task<BaseResponseDto> UpdateCityAsync(RequestUpdateCityDto request, Guid id)
        {
            // Find the city by ID
            var city = _unitOfWork.CityRepository!.GetByIdAsync(id).Result;

            // If the city not found
            if (city == null || city.IsDeleted)
            {
                return Task.FromResult(new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "City không tồn tại.",
                });
            }

            // Map the request data to the city entity
            city.Name = request.Name ?? city.Name;
            city.Region = request.Region ?? city.Region;
            city.IsActive = request.IsActive ?? city.IsActive;
            city.Order = request.Order ?? city.Order;

            // Save the changes to the database
            _unitOfWork.CityRepository.Update(city);

            _unitOfWork.SaveChangesAsync();
            return Task.FromResult(new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật thành công.",
            });
        }

        public async Task<BaseResponseDto> UploadCityAsync(RequestUploadCityDto request)
        {
            if (request == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Không có thành phố nào được tải lên.",
                };
            }

            var predicate = PredicateBuilder.New<City>(x => !x.IsDeleted);
            var cities = await _unitOfWork.CityRepository!.GetAllAsync(predicate);
            var orders = cities.Select(x => x.Order).ToList();
            var maxOrder = orders.Count() > 0 ? orders.Max() : 0;

            // Check if the Order is null or not
            if (!request.Order.HasValue || request.Order == null || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data from request to entity
            var banner = _mapper.Map<City>(request);
            banner.IsActive = true;

            // Save the banner to the database
            await _unitOfWork.CityRepository!.AddAsync(banner);


            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Tải thành phố lên thành công.",
            };
        }
    }
}
