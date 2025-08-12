using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;
using MemoryHotelApi.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IAuthService : IGenericService<User>
    {
        Task<ResponseSendOtpDto> SendOtpAsync(RequestSendOtpDto request);
        Task<ResponseLoginDto> LoginAsync(RequestLoginDto request);
        Task<ResponseLoginDto> RefreshTokenAsync(RequestRefreshTokenDto request);
        Task<ResponseRegisterDto> RegisterAsync(RequestRegisterDto request);
        Task<ResponseVerifyOtpDto> VerifyOtpAsync(RegisterVerifyOtpRequestDto request);
        Task<ResponseLoginDto> SetNewPassword(RequestSetPasswordDto request);
        Task<ResponseResetPasswordDto> ResetPassword(RequestResetPasswordDto request);
    }
}
