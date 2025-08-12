using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto
{
    public class RequestUploadCityDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên thành phố!")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vùng miền!")]
        public required string Region { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thứ tự!")]
        public int? Order { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã vùng miền!")]
        [RegularExpression(@"^(north|central|south)$", ErrorMessage = "Mã vùng miền không hợp lệ!")]
        public required string RegionKey { get; set; }
    }
}
