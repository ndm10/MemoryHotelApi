using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common;
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
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtUtility _jwtUtility;
        private readonly IMemoryCache _memoryCache;
        private readonly EmailSender _emailSender;

        private readonly TimeSpan _otpExpiration = TimeSpan.FromMinutes(3);
        private readonly TimeSpan _lockoutDuration = TimeSpan.FromSeconds(10);

        public AuthService(IMapper mapper, PasswordHasher passwordHasher, IUnitOfWork unitOfWork, JwtUtility jwtUtility, IMemoryCache memoryCache, EmailSender emailSender) : base(mapper, unitOfWork)
        {
            _passwordHasher = passwordHasher;
            _jwtUtility = jwtUtility;
            _memoryCache = memoryCache;
            _emailSender = emailSender;
        }

        public async Task<ResponseLoginDto> LoginAsync(RequestLoginDto request)
        {
            var response = new ResponseLoginDto();

            // Find the user by username
            var user = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);

            // If login information is invalid
            if (user == null || user.IsVerified == false)
            {
                response.StatusCode = 400;
                response.IsSuccess = false;
                response.Message = "Tài khoản chưa được đăng ký, vui lòng thử lại!";
                return response;
            }

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            {
                response.StatusCode = 400;
                response.IsSuccess = false;
                response.Message = "Email hoặc mật khẩu không đúng. Vui lòng thử lại!";
                return response;
            }

            return await CreateTokenAsync(user);
        }

        public async Task<ResponseLoginDto> RefreshTokenAsync(RequestRefreshTokenDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user == null)
            {
                return new ResponseLoginDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Refresh token không hợp lệ hoặc đã hết hạn."
                };
            }

            return await CreateTokenAsync(user);
        }

        public async Task<ResponseRegisterDto> RegisterAsync(RequestRegisterDto request)
        {
            var userRepository = _unitOfWork.UserRepository!;

            // Check if email of the user exist or not
            var existingUser = await userRepository.FindUserByEmail(request.Email);

            // Check if user verified account or not
            if (existingUser != null && existingUser.IsVerified)
            {
                return new ResponseRegisterDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email này đã được đăng ký!"
                };
            }

            // Mapping data
            var user = _mapper.Map<User>(request);

            // Save change to DB and send OTP to email
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                // Generate OTP and save it to cache
                string otp = new Random().Next(100000, 999999).ToString();
                string otpKey = $"{request.Email}_{request.ClientIp}_OTP";
                _memoryCache.Set(otpKey, otp, _otpExpiration);

                // Send OTP to email
                await _emailSender.SendOtpRegisterAsync(request.Email, otp);

                if (existingUser == null)
                {
                    // Setting other properties
                    // Get the role of user
                    var userRole = await _unitOfWork.RoleRepository!.FindRoleByNameAsync(Constants.RoleUserName);
                    if (userRole == null)
                    {
                        return new ResponseRegisterDto
                        {
                            StatusCode = 500,
                            Message = "Có lỗi xảy ra với phần quyền người dùng. Vui lòng thử lại sau!"
                        };
                    }

                    // Setting values
                    user.IsVerified = false;
                    user.RoleId = userRole.Id;
                    user.Password = string.Empty;
                    await userRepository.AddAsync(user);
                }
                else
                {
                    existingUser.FullName = request.FullName;
                    existingUser.Phone = request.Phone;
                }

                // Save change to DB
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ResponseRegisterDto
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Đăng ký thành công. Vui lòng kiểm tra email mã OTP để xác thực tài khoản của bạn.",
                };
            }
        }

        public async Task<ResponseSendOtpDto> SendOtpAsync(RequestSendOtpDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResponseSendOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Xin vui lòng nhập Email nhận OTP."
                };
            }

            if (string.IsNullOrEmpty(request.ClientIp))
            {
                return new ResponseSendOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Không xác thực được người dùng, vui lòng thử lại sau!"
                };
            }

            // Check if email of the user exist or not
            var existingUser = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);
            if (existingUser == null)
            {
                return new ResponseSendOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email này chưa được đăng ký. Vui lòng sử dụng email khác."
                };
            }

            // Generate OTP
            string otp = new Random().Next(100000, 999999).ToString();
            string otpKey = $"{request.Email}_{request.ClientIp}_OTP";
            _memoryCache.Set(otpKey, otp, _otpExpiration);

            // Reset failed attempts IP
            string failedAttemptsKey = $"FailedAttempts_IP_{request.ClientIp}";
            _memoryCache.Remove(failedAttemptsKey);

            // Send OTP to email
            var sendOtpResponse = _emailSender.SendOtpResetPasswordAsync(request.Email, otp);

            return new ResponseSendOtpDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Mã OTP đã được gửi đến email của bạn!"
            };
        }

        public async Task<ResponseVerifyOtpDto> VerifyOtpAsync(RegisterVerifyOtpRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Otp))
            {
                return new ResponseVerifyOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Xin vuil lòng nhập Otp."
                };
            }

            if (string.IsNullOrEmpty(request.ClientIp))
            {
                return new ResponseVerifyOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Không xác thực được người dùng, vui lòng thử lại sau!"
                };
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResponseVerifyOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Xin vui lòng nhập Email."
                };
            }


            string failedAttemptsKey = $"FailedAttempts_IP_{request.ClientIp}";

            string lockoutKey = $"Lockout_IP_{request.ClientIp}";
            if (_memoryCache.TryGetValue(lockoutKey, out DateTime lockoutEnd) && lockoutEnd > DateTime.UtcNow)
            {
                var remainingTime = (lockoutEnd - DateTime.UtcNow).TotalMinutes;
                return new ResponseVerifyOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = $"Bạn đã nhập sai OTP nhiều lần, vui lòng thử lại sau {remainingTime:F1} phút."
                };
            }

            string otpKey = $"{request.Email}_{request.ClientIp}_OTP";
            if (!_memoryCache.TryGetValue(otpKey, out string? storedOtp) || string.IsNullOrEmpty(storedOtp))
            {
                return new ResponseVerifyOtpDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "OTP không hợp lệ hoặc đã hết hạn."
                };
            }

            if (request.Otp == storedOtp)
            {
                _memoryCache.Remove(otpKey);
                _memoryCache.Remove(failedAttemptsKey);

                // Save OTP to verify in step 2
                var otpKeyForStepTwo = $"{request.Email}_{request.ClientIp}_OTP_VERIFY";
                _memoryCache.Set(otpKeyForStepTwo, request.Otp, _otpExpiration);

                return new ResponseVerifyOtpDto
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Xác thực OTP thành công!",
                };
            }
            else
            {
                // If OTP is incorrect, increment failed attempts
                int failedAttempts = _memoryCache.TryGetValue(failedAttemptsKey, out int attempts) ? attempts + 1 : 1;
                _memoryCache.Set(failedAttemptsKey, failedAttempts, _otpExpiration);

                if (failedAttempts >= Constants.MaxFailedAttempts)
                {
                    _memoryCache.Set(lockoutKey, DateTime.UtcNow.Add(_lockoutDuration), _lockoutDuration);
                    return new ResponseVerifyOtpDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Bạn đã nhập sai OTP nhiều lần, vui lòng thử lại sau {_lockoutDuration.TotalMinutes} phút."
                    };
                }
                else
                {
                    return new ResponseVerifyOtpDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"OTP không đúng, bạn còn {Constants.MaxFailedAttempts - failedAttempts} lần thử",
                    };
                }
            }
        }

        public async Task<ResponseLoginDto> SetNewPassword(RequestSetPasswordDto request)
        {
            // Check if user verified OTP or not
            var otpKey = $"{request.Email}_{request.ClientIp}_OTP_VERIFY";
            if (!_memoryCache.TryGetValue(otpKey, out string? storedOtp) || string.IsNullOrEmpty(storedOtp))
            {
                return new ResponseLoginDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Bạn chưa xác thực OTP vui lòng thực hiện lại!"
                };
            }

            // Find user by email
            var user = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);
            if (user == null)
            {
                return new ResponseLoginDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email này chưa được đăng ký!"
                };
            }

            string failedAttemptsKey = $"FailedAttempts_IP_{request.ClientIp}";
            string lockoutKey = $"Lockout_IP_{request.ClientIp}";

            // Check OTP entered with the OTP stored in cache
            if (request.Otp == storedOtp)
            {
                _memoryCache.Remove(otpKey);
                _memoryCache.Remove(failedAttemptsKey);

                // Enable user and set password
                user.IsVerified = true;
                user.Password = _passwordHasher.HashPassword(request.Password);
                await _unitOfWork.SaveChangesAsync();

                return await CreateTokenAsync(user);
            }
            else
            {
                // If OTP is incorrect, increment failed attempts
                int failedAttempts = _memoryCache.TryGetValue(failedAttemptsKey, out int attempts) ? attempts + 1 : 1;
                _memoryCache.Set(failedAttemptsKey, failedAttempts, _otpExpiration);

                if (failedAttempts >= Constants.MaxFailedAttempts)
                {
                    _memoryCache.Set(lockoutKey, DateTime.UtcNow.Add(_lockoutDuration), _lockoutDuration);
                    return new ResponseLoginDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Bạn đã nhập sai OTP nhiều lần, vui lòng thử lại sau {_lockoutDuration.TotalMinutes} phút."
                    };
                }
                else
                {
                    return new ResponseLoginDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"OTP không đúng, bạn còn {Constants.MaxFailedAttempts - failedAttempts} lần thử",
                    };
                }
            }
        }

        public async Task<ResponseResetPasswordDto> ResetPassword(RequestResetPasswordDto request)
        {
            // Check if user verified OTP or not
            var otpKey = $"{request.Email}_{request.ClientIp}_OTP_VERIFY";
            if (!_memoryCache.TryGetValue(otpKey, out string? storedOtp) || string.IsNullOrEmpty(storedOtp))
            {
                return new ResponseResetPasswordDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Bạn chưa xác thực OTP vui lòng thực hiện lại!"
                };
            }

            // Find user by email
            var user = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);
            if (user == null)
            {
                return new ResponseResetPasswordDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email này chưa được đăng ký!"
                };
            }

            string failedAttemptsKey = $"FailedAttempts_IP_{request.ClientIp}";
            string lockoutKey = $"Lockout_IP_{request.ClientIp}";

            // Check OTP entered with the OTP stored in cache
            if (request.Otp == storedOtp)
            {
                _memoryCache.Remove(otpKey);
                _memoryCache.Remove(failedAttemptsKey);

                // Enable user and set password
                user.IsVerified = true;
                user.Password = _passwordHasher.HashPassword(request.Password);
                await _unitOfWork.SaveChangesAsync();

                return new ResponseResetPasswordDto
                {
                    StatusCode = 200,
                    IsSuccess = true,
                    Message = "Đổi mật khẩu thành công!",
                };
            }
            else
            {
                // If OTP is incorrect, increment failed attempts
                int failedAttempts = _memoryCache.TryGetValue(failedAttemptsKey, out int attempts) ? attempts + 1 : 1;
                _memoryCache.Set(failedAttemptsKey, failedAttempts, _otpExpiration);

                if (failedAttempts >= Constants.MaxFailedAttempts)
                {
                    _memoryCache.Set(lockoutKey, DateTime.UtcNow.Add(_lockoutDuration), _lockoutDuration);
                    return new ResponseResetPasswordDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"Bạn đã nhập sai OTP nhiều lần, vui lòng thử lại sau {_lockoutDuration.TotalMinutes} phút."
                    };
                }
                else
                {
                    return new ResponseResetPasswordDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = $"OTP không đúng, bạn còn {Constants.MaxFailedAttempts - failedAttempts} lần thử",
                    };
                }
            }
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
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return refreshToken;
            }
        }

        private async Task<ResponseLoginDto> CreateTokenAsync(User user)
        {
            var response = new ResponseLoginDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Token = _jwtUtility.GenerateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                UserId = user.Id,
                FullName = user.FullName,
                Phone = user.Phone,
                ExpiredTimeToken = DateTime.UtcNow.AddDays(Constants.TokenExpiredTime),
                ExpiredTimeRefreshToken = DateTime.UtcNow.AddDays(7)
            };
            return response;
        }
    }
}
