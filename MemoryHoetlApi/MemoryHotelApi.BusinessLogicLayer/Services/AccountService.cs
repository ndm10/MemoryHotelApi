using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseChangePasswordDto> ChangePassword(RequestChangePasswordDto request)
        {
            var userRepository = _unitOfWork.UserRepository!;

            // Get user by userId
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new ResponseChangePasswordDto
                {
                    IsSuccess = false,
                    Message = "Có lỗi khi xác thực tài khoản!"
                };
            }

            // Check if the old password is correct
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                return new ResponseChangePasswordDto
                {
                    IsSuccess = false,
                    Message = "Mật khẩu cũ không chính xác!"
                };
            }

            // Hash the new password
            var hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Password = hashedNewPassword;

            // Update the user in the database
            await _unitOfWork.SaveChangesAsync();

            return new ResponseChangePasswordDto
            {
                IsSuccess = true,
                Message = "Đổi mật khẩu thành công!"
            };
        }

        public async Task<ResponseUpdateProfileDto> UpdateProfile(RequestUpdateProfileDto request)
        {
            var userRepository = _unitOfWork.UserRepository!;

            // Get user by userId
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                return new ResponseUpdateProfileDto
                {
                    IsSuccess = false,
                    Message = "Có lỗi khi xác thực tài khoản!"
                };
            }

            // Update user profile
            user.FullName = request.FullName;
            user.Phone = request.Phone;
            user.Nationality = request.Nationality;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new ResponseUpdateProfileDto
            {
                IsSuccess = true,
                Message = "Cập nhật thông tin thành công!"
            };
        }
    }
}
