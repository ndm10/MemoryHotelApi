using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi khi xác thực tài khoản!"
                };
            }

            // Check if the old password is correct
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                return new ResponseChangePasswordDto
                {
                    StatusCode = 400,
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
                StatusCode = 200,
                IsSuccess = true,
                Message = "Đổi mật khẩu thành công!"
            };
        }

        public async Task<ResponseGetProfileDto> GetProfile(string userId)
        {
            // Get user by userId
            var user = await _unitOfWork.UserRepository!.GetUserProfileAsync(userId);

            if (user == null)
            {
                return new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi khi xác thực tài khoản!"
                };
            }

            var response = _mapper.Map<ResponseGetProfileDto>(user);

            if (response.MembershipTier != null)
            {
                var predicate = PredicateBuilder.New<MembershipTier>(x => x.IsDeleted == false && x.IsActive == true && x.Order >= user.MembershipTier!.Order);

                var includes = new string[]
                {
                nameof(MembershipTier.Benefits),
                };

                // Find next membership tier by the order property
                var membershipTiers = await _unitOfWork.MembershipTierRepository!.GetAllAsync(predicate, includes);


                if (membershipTiers == null)
                {
                    response.NextMembershipTier = null;
                }
                else
                {
                    response.NextMembershipTier = _mapper.Map<MembershipTierCommonDto>(membershipTiers.OrderBy(x => x.Order).FirstOrDefault(x => x.Id != user.MembershipTier!.Id));
                }
            }

            // Map user to ResponseGetProfileDto
            response.StatusCode = 200;
            response.IsSuccess = true;
            return response;
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
                    StatusCode = 500,
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
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật thông tin thành công!"
            };
        }
    }
}
