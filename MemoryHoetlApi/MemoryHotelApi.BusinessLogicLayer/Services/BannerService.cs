using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
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

        public async Task<GenericResponseDto> SoftDeleteAsync(Guid id)
        {
            try
            {
                var banner = await _unitOfWork.BannerRepository!.GetByIdAsync(id);

                if (banner == null)
                {
                    return new GenericResponseDto
                    {
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
                    IsSuccess = true,
                    Message = "Xóa banner thành công.",
                };
            }
            catch
            {
                // Handle exceptions and return an appropriate response
                return new GenericResponseDto
                {
                    IsError = false,
                    Message = "Đã có lỗi xảy ra trong hệ thống.",
                };
            }
        }

        public async Task<GenericResponseDto> UploadBanner(RequestUploadBannerDto request)
        {
            try
            {
                if (request.Banners == null || request.Banners.Count == 0)
                {
                    return new GenericResponseDto
                    {
                        IsSuccess = false,
                        Message = "Không có banner nào được tải lên.",
                    };
                }

                var maxOrder = await _unitOfWork.BannerRepository!.GetMaxOrder();

                foreach (var r in request.Banners)
                {
                    // Check if the Order is null or not
                    if (!r.Order.HasValue || r.Order == null || r.Order == 0)
                    {
                        // If Order is null, set it to the maximum value in the database
                        maxOrder = maxOrder + 1;
                        r.Order = maxOrder;
                    }

                    // Mapping data from request to entity
                    var banner = _mapper.Map<Banner>(r);

                    // Save the banner to the database
                    await _unitOfWork.BannerRepository!.AddAsync(banner);
                }
                 
                await _unitOfWork.SaveChangesAsync();

                return new GenericResponseDto
                {
                    IsSuccess = true,
                    Message = "Tải banner lên thành công.",
                };
            }
            catch
            {
                // Handle exceptions and return an appropriate response
                return new GenericResponseDto
                {
                    IsError = false,
                    Message = "Đã có lỗi xảy ra trong hệ thống.",
                };
            }
        }
    }
}
