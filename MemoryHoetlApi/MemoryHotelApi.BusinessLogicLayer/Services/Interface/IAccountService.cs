using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IAccountService
    {
        Task<ResponseUpdateProfileDto> UpdateProfile(RequestUpdateProfileDto request);
        Task<ResponseChangePasswordDto> ChangePassword(RequestChangePasswordDto request);
    }
}
