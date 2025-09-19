using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.DataAccessLayer.Entities;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto
{
    public class ResponseGetMembershipTiersDto : GenericResponsePaginationDto<MembershipTierDto>
    {
    }

    public class MembershipTierDto
    {
        public Guid Id { get; set; }
        public string Icon { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<MembershipTierMembershipTierBenefitDto> Benefits { get; set; } = new List<MembershipTierMembershipTierBenefitDto>();
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleteAllowed { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
