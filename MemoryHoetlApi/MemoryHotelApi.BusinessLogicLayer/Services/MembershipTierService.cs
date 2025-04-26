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
    public class MembershipTierService : GenericService<MembershipTier>, IMembershipTierService
    {
        private readonly StringUtility _stringUtility;

        public MembershipTierService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
        }

        public async Task<ResponseGetMembershipTierDto> GetMembershipTierAsync(Guid id)
        {
            // Check if the membership tier exists
            var membershipTier = await _unitOfWork.MembershipTierRepository!.GetMembershipTierByIdAsync(id);

            if (membershipTier == null || membershipTier.IsDeleted)
            {
                return new ResponseGetMembershipTierDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng thành viên!",
                    IsSuccess = false
                };
            }

            // Map the membership tier to the response DTO
            var membershipTierDto = _mapper.Map<MembershipTierDto>(membershipTier);
            return new ResponseGetMembershipTierDto
            {
                StatusCode = 200,
                Data = membershipTierDto,
                IsSuccess = true
            };
        }

        public async Task<ResponseGetMembershipTiersDto> GetMembershipTiersAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Set default values for pageIndex and pageSize if they are null
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<MembershipTier>(x => !x.IsDeleted);

            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the membership tiers from the database
            var membershipTiers = await _unitOfWork.MembershipTierRepository!.GetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)membershipTiers.Count() / pageSizeValue);
            var totalRecords = membershipTiers.Count();

            // Map the membership tiers to the response DTO
            var membershipTierDtos = _mapper.Map<List<MembershipTierDto>>(membershipTiers.OrderBy(x => x.Order));

            return new ResponseGetMembershipTiersDto
            {
                StatusCode = 200,
                Data = membershipTierDtos,
                TotalPage = totalPages,
                TotalRecord = totalRecords,
            };
        }

        public async Task<BaseResponseDto> SoftDeleteMembershipTierAsync(Guid id)
        {
            // Check if the membership tier exists
            var membershipTier = await _unitOfWork.MembershipTierRepository!.GetByIdAsync(id);

            if (membershipTier == null || membershipTier.IsDeleted)
            {
                return new ResponseGetMembershipTierDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng thành viên!",
                    IsSuccess = false
                };
            }

            // Update the membership tier to soft delete it
            membershipTier.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();

            return new ResponseGetMembershipTierDto
            {
                StatusCode = 200,
                Message = "Xóa hạng thành viên thành công!",
                IsSuccess = true
            };
        }

        public async Task<BaseResponseDto> UpdateMembershipTierAsync(RequestUpdateMembershipTierDto request, Guid id)
        {
            // Check if the membership tier exists
            var membershipTier = await _unitOfWork.MembershipTierRepository!.GetMembershipTierByIdAsync(id);

            if (membershipTier == null || membershipTier.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng thành viên!",
                    IsSuccess = false
                };
            }

            var membershipTierBenefits = new List<MembershipTierBenefit>();
            Dictionary<MembershipTierBenefit, string?> benefitDic = new Dictionary<MembershipTierBenefit, string?>();
            // Check if the benefit exists or not
            if (request.Benefits != null && request.Benefits.Count > 0)
            {
                // Find the benefits by their IDs
                foreach (var benefit in request.Benefits)
                {
                    var benefitEntity = await _unitOfWork.MembershipTierBenefitRepository!.GetByIdAsync(benefit.Id);
                    // If benfit is not found or is deleted, return an error response
                    if (benefitEntity == null || benefitEntity.IsDeleted)
                    {
                        return new BaseResponseDto
                        {
                            StatusCode = 404,
                            IsSuccess = false,
                            Message = "Không tìm thấy quyền lợi thành viên!",
                        };
                    }

                    benefitDic.Add(benefitEntity, benefit.Value);
                    membershipTierBenefits.Add(benefitEntity);
                }

                // Clear the existing benefits
                membershipTier.Benefits.Clear();

                // Update new benefits
                foreach (var benefitEntity in membershipTierBenefits)
                {
                    var membershipTierMembershipTierBenefit = new MembershipTierMembershipTierBenefit
                    {
                        MembershipTier = membershipTier,
                        MembershipTierBenefit = benefitEntity,
                        Value = benefitDic.GetValueOrDefault(benefitEntity)

                    };

                    membershipTier.Benefits.Add(membershipTierMembershipTierBenefit);
                }
            }

            // Check the Name is null or empty
            if (!string.IsNullOrEmpty(request.Name))
            {
                // Format the name to remove extra spaces and uppercase each word
                membershipTier.Name = _stringUtility.UpperCaseFirstEachWord(request.Name);
            }

            // Update other properties
            membershipTier.Icon = request.Icon ?? membershipTier.Icon;
            membershipTier.Description = request.Description ?? membershipTier.Description;
            membershipTier.IsActive = request.IsActive ?? membershipTier.IsActive;
            membershipTier.Order = request.Order ?? membershipTier.Order;

            // Save the changes
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật hạng thành viên thành công.",
            };
        }

        public async Task<BaseResponseDto> UploadMembershipTierAsync(RequestUploadMembershipTierDto request)
        {
            // Check the benefit exist or not
            var membershipTierBenefits = new List<MembershipTierBenefit>();
            Dictionary<MembershipTierBenefit, string?> benefitDic = new Dictionary<MembershipTierBenefit, string?>();
            if (request.Benefits != null && request.Benefits.Count > 0)
            {
                foreach (var benefit in request.Benefits)
                {
                    var benefitEntity = await _unitOfWork.MembershipTierBenefitRepository!.GetByIdAsync(benefit.Id);
                    if (benefitEntity == null || benefitEntity.IsDeleted)
                    {
                        return new BaseResponseDto
                        {
                            StatusCode = 404,
                            IsSuccess = false,
                            Message = "Không tìm thấy quyền lợi thành viên!",
                        };
                    }

                    membershipTierBenefits.Add(benefitEntity);

                    // Add to dictionary with value
                    benefitDic.Add(benefitEntity, benefit.Value);
                }
            }

            // Create the order of membership tier
            var predicate = PredicateBuilder.New<MembershipTier>(x => !x.IsDeleted);
            var membershipTiers = await _unitOfWork.MembershipTierRepository!.GetAllAsync(predicate);
            int order = 1;
            if (membershipTiers != null && membershipTiers.Count() > 0)
            {
                order = membershipTiers.Max(x => x.Order) + 1;
            }

            if (!request.Order.HasValue || request.Order.Value == 0)
            {
                request.Order = order;
            }

            // Format the name to remove extra spaces and uppercase each word
            request.Name = _stringUtility.UpperCaseFirstEachWord(request.Name);

            // Mapping the request to the membership tier entity
            var membershipTier = _mapper.Map<MembershipTier>(request);

            foreach (var benefitEntity in membershipTierBenefits)
            {
                var membershipTierMembershipTierBenefit = new MembershipTierMembershipTierBenefit
                {
                    MembershipTier = membershipTier,
                    MembershipTierBenefit = benefitEntity,
                    Value = benefitDic.GetValueOrDefault(benefitEntity)
                };

                membershipTier.Benefits.Add(membershipTierMembershipTierBenefit);
            }

            // Add the membership tier to the database
            await _unitOfWork.MembershipTierRepository!.AddAsync(membershipTier);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm hạng thành viên thành công.",
            };
        }
    }
}
