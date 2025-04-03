using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AuthenticationDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class AuthService : GenericService<User>, IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtUtility _jwtUtility;

        public AuthService(IMapper mapper, PasswordHasher passwordHasher, IUnitOfWork unitOfWork, JwtUtility jwtUtility) : base(mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtUtility = jwtUtility;
        }

        public async Task<ResponseLoginDto> LoginAsync(RequestLoginDto request)
        {
            var response = new ResponseLoginDto();

            // Find the user by username
            var user = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);

            // If login information is invalid
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                return response;
            }

            return await CreateTokenAsync(user);
        }

        public async Task<ResponseLoginDto> RefreshTokenAsync(RequestRefreshTokenDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user == null)
            {
                return new ResponseLoginDto();
            }

            return await CreateTokenAsync(user);
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _unitOfWork.UserRepository!.FindUserByRefreshToken(userId, refreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var refreshToken = _jwtUtility.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _unitOfWork.SaveChangeAsync();
                await transaction.CommitAsync();
                return refreshToken;
            }
        }

        private async Task<ResponseLoginDto> CreateTokenAsync(User user)
        {
            var response = new ResponseLoginDto
            {
                Token = _jwtUtility.GenerateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                UserId = user.Id,
                FullName = user.FullName,
                Phone = user.Phone,
            };
            return response;
        }

        public Task<ResponseRegisterDto> RegisterAsync(RequestRegisterDto request)
        {
            throw new NotImplementedException();
        }
    }
}
