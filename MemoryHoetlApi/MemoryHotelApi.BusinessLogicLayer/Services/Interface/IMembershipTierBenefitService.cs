

using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IMembershipTierBenefitService
    {
        Task<ResponseGetMembershipTierBenefitDto> GetMembershipTierBenefitAsync(Guid id);
        Task<ResponseGetMembershipTierBenefitsDto> GetMembershipTierBenefitsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status);
        Task<BaseResponseDto> SoftDeleteMembershipTierBenefitAsync(Guid id);
        Task<BaseResponseDto> UpdateMembershipTierBenefitAsync(RequestUpdateMembershipTierBenefitDto request, Guid id);
        Task<BaseResponseDto> UploadMembershipTierBenefitAsync(RequestUploadMembershipTierBenefitDto request);
    }
}
