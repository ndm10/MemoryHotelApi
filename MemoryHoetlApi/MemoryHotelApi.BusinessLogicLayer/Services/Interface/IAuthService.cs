using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;

namespace MemoryHotelApi.BusinessLogicLayer.Services.Interface
{
    public interface IAuthService: IGenericService
    {
        Task<ResponseLoginDto> LoginAsync(RequestLoginDto request);
        Task<ResponseLoginDto> RefreshTokenAsync(RequestRefreshTokenDto request);
        Task<ResponseRegisterDto> RegisterAsync(RequestRegisterDto request);
    }
}
