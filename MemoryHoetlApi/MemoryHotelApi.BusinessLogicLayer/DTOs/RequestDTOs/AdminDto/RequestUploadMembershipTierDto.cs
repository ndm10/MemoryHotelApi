using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadMembershipTierDto
    {
        [Required(ErrorMessage = "Vui lòng cung cấp ảnh cho hạng thành viên")]
        public string Icon { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập tên cho hạng thành viên")]
        public string Name { get; set; } = null!;
     
        public string? Description { get; set; }
        public int? Order { get; set; }

        public List<Benefit>? Benefits { get; set; }
    }

    public class Benefit
    {
        public Guid Id { get; set; }
        public string? Value { get; set; }
    }
}
