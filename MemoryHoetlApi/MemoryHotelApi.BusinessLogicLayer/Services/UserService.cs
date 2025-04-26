using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<ResponseGetMembershipDto> GetMembershipAsync(Guid id)
        {
            // Find the User by id
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(id);

            // If user is null, return a not found response
            if (user == null || user.IsDeleted)
            {
                return new ResponseGetMembershipDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Người dùng không tồn tại.",
                };
            }

            // Map the user to the response DTO
            var membershipDto = _mapper.Map<MembershipDto>(user);
            
            return new ResponseGetMembershipDto
            {
                StatusCode = 200,
                Data = membershipDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetMembershipsDto> GetMembershipsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Set default values for pageIndex and pageSize if they are null
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<User>(x => !x.IsDeleted && x.Role.Name.Equals(Constants.RoleUserName));

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

            // Get all the memberships from the database
            var memberships = await _unitOfWork.UserRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)memberships.Count() / pageSizeValue);
            var totalRecord = memberships.Count();

            return new ResponseGetMembershipsDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<MembershipDto>>(memberships.OrderBy(x => x.FullName)),
                TotalPage = totalPages,
                TotalRecord = totalRecord
            };
        }
    }
}
