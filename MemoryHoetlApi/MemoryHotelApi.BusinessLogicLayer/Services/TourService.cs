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
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TourService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetTourDto> GetTourAsync(Guid id)
        {
            // Find tour by id
            var tour = await _unitOfWork.TourRepository!.GetTourByIdAsync(id);

            // If tour is null
            if (tour is null)
            {
                return new ResponseGetTourDto
                {
                    StatusCode = 404,
                    Message = "Tour not found",
                    IsSuccess = false,
                };
            }

            // Map tour to DTO
            var tourDto = _mapper.Map<GetTourDto>(tour);

            // Return response
            return new ResponseGetTourDto
            {
                StatusCode = 200,
                Data = tourDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetToursDto> GetToursAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? cityId)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<Tour>(x => !x.IsDeleted);

            // Check if textSearch is not null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                // Add the search condition to the predicate
                predicate = predicate.And(x => x.Title.Contains(textSearch, StringComparison.OrdinalIgnoreCase) || x.SubTitle.Contains(textSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Check if status has value
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status.Value);
            }

            // Check if cityId has value
            if (cityId.HasValue)
            {
                predicate = predicate.And(x => x.CityId == cityId.Value);
            }

            var tours = await _unitOfWork.TourRepository!.GetTourPaginationAsync(pageIndexValue, pageSizeValue, predicate);
            var toursDto = _mapper.Map<List<GetTourDto>>(tours.OrderBy(x => x.Order));

            return new ResponseGetToursDto
            {
                StatusCode = 200,
                Data = toursDto,
                IsSuccess = true,
                TotalCount = tours.Count,
                TotalPage = (int)Math.Ceiling((double)tours.Count / pageSizeValue),
            };

        }

        public async Task<BaseResponseDto> SoftDeleteTourAsync(Guid id)
        {
            // Find the tour by id
            var tour = await _unitOfWork.TourRepository!.GetByIdAsync(id);

            // If tour is null
            if (tour is null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Tour not found",
                    IsSuccess = false,
                };
            }

            // Soft delete the tour
            tour.IsDeleted = true;

            // Update the tour to database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Xóa tour thành công",
                IsSuccess = true,
            };
        }

        public async Task<BaseResponseDto> UpdateTourAsync(RequestUpdateTourDto request, Guid id)
        {
            var includes = new string[] {
                nameof(Tour.Images),
            };

            // Find tour by the id
            var tour = await _unitOfWork.TourRepository!.GetByIdIncludeAsync(id, includes);

            // If tour is not found
            if (tour is null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy tour",
                    IsSuccess = false,
                };
            }

            // If tour is found, update the tour
            tour.Title = request.Title ?? tour.Title;
            tour.SubTitle = request.SubTitle ?? tour.SubTitle;
            tour.Description = request.Description ?? tour.Description;
            tour.CityId = request.CityId ?? tour.CityId;
            tour.IsActive = request.IsActive ?? tour.IsActive;
            tour.Order = request.Order ?? tour.Order;

            // Find the image by url
            if (request.ImageUrls != null)
            {
                var images = await _unitOfWork.ImageRepository!.GetImagesWithCondition(x => request.ImageUrls.Contains(x.Url));
                // Check if images is null
                if (images is null || images.Count == 0)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "Có lỗi với việc tả ảnh lên, vui lòng thử lại sau",
                        IsSuccess = false,
                    };
                }
                // Add images to tour
                tour.Images = images;
            }

            // Update tour to database
            _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Cập nhật tour thành công",
                IsSuccess = true,
            };
        }

        public async Task<BaseResponseDto> UploadTourAsync(RequestUploadTourDto request)
        {
            var predicate = PredicateBuilder.New<Tour>(x => !x.IsDeleted);

            var tours = await _unitOfWork.TourRepository!.GetAllAsync(predicate);
            var orders = tours.Select(x => x.Order).ToList();
            var maxOrder = orders.Count() > 0 ? orders.Max() : 0;

            // Check if the Order is null or not
            if (!request.Order.HasValue || !request.Order.HasValue || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data to tour entity
            var tour = _mapper.Map<Tour>(request);

            // Find the image by url
            var images = await _unitOfWork.ImageRepository!.GetImagesWithCondition(x => request.ImageUrls.Contains(x.Url));

            // Check if images is null
            if (images is null || images.Count == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Có lỗi với việc tả ảnh lên, vui lòng thử lại sau",
                    IsSuccess = false,
                };
            }

            // Add images to tour
            foreach (var image in images)
            {
                tour.Images.Add(image);
            }

            tour.IsActive = true;

            // Add tour to database
            await _unitOfWork.TourRepository!.AddAsync(tour);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Tải lên tour thành công",
                IsSuccess = true,
            };
        }
    }
}
