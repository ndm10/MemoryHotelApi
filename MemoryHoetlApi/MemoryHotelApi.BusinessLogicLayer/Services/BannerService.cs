using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BannerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetBannerDto> GetBannerAsync(Guid id)
        {
            var banner = await _unitOfWork.BannerRepository!.GetByIdAsync(id);
            if (banner == null || banner.IsDeleted)
            {
                return new ResponseGetBannerDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Banner không tồn tại.",
                };
            }

            // Map the banner to the response DTO
            var bannerDto = _mapper.Map<GetBannerDto>(banner);
            return new ResponseGetBannerDto
            {
                StatusCode = 200,
                Data = bannerDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetBannersDto> GetBannersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Get all the banners from the database
            var banners = await _unitOfWork.BannerRepository!.GetPagination(pageIndexValue, pageSizeValue, textSearch, status);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)banners.Count() / pageSizeValue);

            // Map the banners to the response DTO
            return new ResponseGetBannersDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<GetBannerDto>>(banners),
                TotalPage = totalPages,
                TotalRecord = banners.Count(),
                IsSuccess = true,
                Message = "Lấy danh sách banner thành công.",
            };
        }

        public async Task<GenericResponseDto> SoftDeleteAsync(Guid id)
        {
            var banner = await _unitOfWork.BannerRepository!.GetByIdAsync(id);

            if (banner == null || banner.IsDeleted)
            {
                return new GenericResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Banner không tồn tại.",
                };
            }

            // Soft delete the banner
            banner.IsDeleted = true;

            // Update the banner in the database
            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa banner thành công.",
            };
        }

        public async Task<GenericResponseDto> UpdateBannerAsync(RequestUpdateBannerDto request, Guid id)
        {
            // Find the banner by ID
            var banner = await _unitOfWork.BannerRepository!.GetByIdAsync(id);

            // Check if the banner exists
            if (banner == null || banner.IsDeleted)
            {
                return new GenericResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Banner không tồn tại.",
                };
            }

            // Map the request data to the banner entity
            banner.ImageUrl = request.ImageUrl ?? banner.ImageUrl;
            banner.Link = request.Link ?? banner.Link;
            banner.Description = request.Description ?? banner.Description;
            banner.Order = request.Order ?? banner.Order;
            banner.IsActive = request.IsActive ?? banner.IsActive;

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật banner thành công.",
            };
        }

        public async Task<GenericResponseDto> UploadBannerAsync(RequestUploadBannerDto request)
        {
            if (request == null)
            {
                return new GenericResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Không có banner nào được tải lên.",
                };
            }

            var maxOrder = await _unitOfWork.BannerRepository!.GetMaxOrder();

            // Check if the Order is null or not
            if (!request.Order.HasValue || request.Order == null || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data from request to entity
            var banner = _mapper.Map<Banner>(request);
            banner.IsActive = true;

            // Save the banner to the database
            await _unitOfWork.BannerRepository!.AddAsync(banner);


            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Tải banner lên thành công.",
            };
        }
    }
}
