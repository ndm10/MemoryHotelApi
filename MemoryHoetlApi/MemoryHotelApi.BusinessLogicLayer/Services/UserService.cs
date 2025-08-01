using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly StringUtility _stringUtility;
        private readonly EmailSender _emailSender;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility, EmailSender emailSender) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
            _emailSender = emailSender;
        }

        public async Task<ResponseGetUserDto> GetUserAsync(Guid id)
        {
            var includes = new string[]
            {
                nameof(User.MembershipTier)
            };

            // Find the User by id
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(id, includes);

            // If user is null, return a not found response
            if (user == null || user.IsDeleted || !user.IsVerified)
            {
                return new ResponseGetUserDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Người dùng không tồn tại.",
                };
            }

            // Map the user to the response DTO
            var membershipDto = _mapper.Map<UserDto>(user);
            
            return new ResponseGetUserDto
            {
                StatusCode = 200,
                Data = membershipDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetUsersDto> GetUsersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, string roleName)
        {
            // Set default values for pageIndex and pageSize if they are null
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<User>(x => !x.IsDeleted && x.Role.Name.Equals(roleName) && x.IsVerified);

            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Email.Contains(textSearch, StringComparison.OrdinalIgnoreCase)
                                            || x.FullName.Contains(textSearch, StringComparison.OrdinalIgnoreCase)
                                            || x.Phone.Contains(textSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            var includes = new string[]
            {
                nameof(User.MembershipTier)
            };

            // Get all the memberships from the database
            var memberships = await _unitOfWork.UserRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // Calculate the total page
            var totalRecords = await _unitOfWork.UserRepository.CountEntities(predicate);
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new ResponseGetUsersDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<UserDto>>(memberships.OrderBy(x => x.FullName)),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<BaseResponseDto> SoftDeleteAsync(Guid id)
        {
            // Find the User by id
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(id);

            // If user is null, return a not found response
            if (user == null || user.IsDeleted || !user.IsVerified)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Người dùng không tồn tại.",
                };
            }

            // Soft delete the user
            user.IsDeleted = true;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa người dùng thành công.",
            };
        }

        public async Task<BaseResponseDto> UpdateAdminAccount(RequestUpdateAdminAccountDto request, Guid id)
        {
            // Find the User by id
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(id);

            // If user is null, return a not found response
            if (user == null || user.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Người dùng không tồn tại.",
                };
            }

            // Update the user properties
            user.IsActive = request.IsActive ?? user.IsActive;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật tài khoản quản trị thành công.",
            };
        }

        public async Task<BaseResponseDto> UpdateMembershipTierAsync(RequestUpdateMembershipTierOfMemberDto request, Guid id)
        {

            // Find the User by id
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(id);

            // If user is null, return a not found response
            if (user == null || user.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Người dùng không tồn tại.",
                };
            }

            // Find the MembershipTier by id
            var membershipTier = await _unitOfWork.MembershipTierRepository!.GetByIdAsync(request.MembershipTierId);

            // If membershipTier is null, return a not found response
            if (membershipTier == null || membershipTier.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Hạng thành viên không tồn tại.",
                };
            }

            // Update the MembershipTier of the user
            user.MembershipTierId = request.MembershipTierId;
            user.MembershipTier = membershipTier;

            // Update the user in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật hạng thành viên thành công.",
            };
        }

        public async Task<BaseResponseDto> UploadAdminAccount(RequestUploadAdminAccountDto request)
        {
            // Check if the email already exists
            var existingUser = await _unitOfWork.UserRepository!.FindUserByEmail(request.Email);

            if (existingUser != null && !existingUser.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Email đã tồn tại.",
                };
            }

            // Mapping to User entity
            var user = _mapper.Map<User>(request);

            // Generate a new password
            var password = _stringUtility.GenerateRandomString();

            // Hash the password
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            user.IsDeletedAllowed = true;
            user.IsVerified = true;

            // Find admin role
            var userRole = await _unitOfWork.RoleRepository!.FindRoleByNameAsync(Constants.RoleAdminName);

            // If userRole is null, return a not found response
            if (userRole == null || userRole.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Vai trò không tồn tại.",
                };
            }

            // Set the user role
            user.RoleId = userRole.Id;
            user.Role = userRole;

            // Save user to the database
            await _unitOfWork.UserRepository!.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Send email with the password
            await _emailSender.SendEmailCreateAdminAsync(user.Email, password);

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Tạo tài khoản quản trị thành công. Mật khẩu đã được gửi tới email người dùng!",
            };

        }
    }
}
