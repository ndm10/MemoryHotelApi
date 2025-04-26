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
using System.Runtime.CompilerServices;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class MembershipTierBenefitService : GenericService<MembershipTierBenefit>, IMembershipTierBenefitService
    {
        private readonly StringUtility _stringUtility = new StringUtility();

        public MembershipTierBenefitService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseGetMembershipTierBenefitDto> GetMembershipTierBenefitAsync(Guid id)
        {
            // Find the membership tier benefit by id
            var membershipTierBenefit = await _unitOfWork.MembershipTierBenefitRepository!.GetByIdAsync(id);

            if (membershipTierBenefit == null || membershipTierBenefit.IsDeleted)
            {
                return new ResponseGetMembershipTierBenefitDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy quyền lợi"
                };
            }

            // Map the membership tier benefit to the response DTO
            var membershipTierBenefitDto = _mapper.Map<MembershipTierBenefitDto>(membershipTierBenefit);

            return new ResponseGetMembershipTierBenefitDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = membershipTierBenefitDto
            };

        }

        public async Task<ResponseGetMembershipTierBenefitsDto> GetMembershipTierBenefitsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Set default values for pageIndex and pageSize if they are null
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate to filter out deleted items
            var predicate = PredicateBuilder.New<MembershipTierBenefit>(x => !x.IsDeleted);

            // Check if textSearch is provided and filter accordingly
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Benefit.Contains(textSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Check if status is provided and filter accordingly
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the membership tier benefits from the database
            var membershipTierBenefits = await _unitOfWork.MembershipTierBenefitRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling((decimal)membershipTierBenefits.Count() / pageSizeValue);

            // Map the results to the DTO and sort them
            var sortedBenefits = membershipTierBenefits.OrderBy(x => x.Order).ToList();

            return new ResponseGetMembershipTierBenefitsDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<MembershipTierBenefitDto>>(sortedBenefits),
                TotalPage = totalPages,
                TotalRecord = membershipTierBenefits.Count()
            };
        }

        public async Task<BaseResponseDto> SoftDeleteMembershipTierBenefitAsync(Guid id)
        {
            // Find the membership tier benefit by id
            var membershipTierBenefit = await _unitOfWork.MembershipTierBenefitRepository!.GetByIdAsync(id);
            if (membershipTierBenefit == null || membershipTierBenefit.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy quyền lợi"
                };
            }

            // Soft delete the membership tier benefit
            membershipTierBenefit.IsDeleted = true;

            // Save changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa quyền lợi thành công"
            };
        }

        public async Task<BaseResponseDto> UpdateMembershipTierBenefitAsync(RequestUpdateMembershipTierBenefitDto request, Guid id)
        {
            // Find the membership tier benefit by id
            var membershipTierBenefit = await _unitOfWork.MembershipTierBenefitRepository!.GetByIdAsync(id);
            if (membershipTierBenefit == null || membershipTierBenefit.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy quyền lợi"
                };
            }

            if (!string.IsNullOrEmpty(request.Benefit))
            {
                membershipTierBenefit.Benefit = _stringUtility.UpperFirstLetter(request.Benefit);
            }

            // Update other properties
            membershipTierBenefit.Order = request.Order ?? membershipTierBenefit.Order;
            membershipTierBenefit.Description = request.Description ?? membershipTierBenefit.Description;
            membershipTierBenefit.IsActive = request.IsActive ?? membershipTierBenefit.IsActive;

            // Save changes to the database
            _unitOfWork.MembershipTierBenefitRepository.Update(membershipTierBenefit);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật quyền lợi thành công"
            };
        }

        public async Task<BaseResponseDto> UploadMembershipTierBenefitAsync(RequestUploadMembershipTierBenefitDto request)
        {
            // Set the order of member ship benefit
            var predicate = PredicateBuilder.New<MembershipTierBenefit>(x => !x.IsDeleted);
            var membershipTierBenefits = await _unitOfWork.MembershipTierBenefitRepository!.GetAllAsync(predicate);
            int order = 1;
            if (membershipTierBenefits != null && membershipTierBenefits.Count() > 0)
            {
                order = membershipTierBenefits.Max(x => x.Order) + 1;
            }

            // Check if the request order have value and different from 0
            if (!request.Order.HasValue || request.Order == 0)
            {
                request.Order = order;
            }

            request.Benefit = _stringUtility.UpperFirstLetter(request.Benefit);

            // Mapping the request to the entity
            var membershipTierBenefit = _mapper.Map<MembershipTierBenefit>(request);

            // Add the membership tier benefit to the database
            await _unitOfWork.MembershipTierBenefitRepository!.AddAsync(membershipTierBenefit);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm quyền lợi thành công"
            };
        }
    }
}
