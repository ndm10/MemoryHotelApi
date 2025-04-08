namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ImageDto
{
    public class ImageUploadRequestDto
    {
        public string FileName { get; set; } = null!;
        public byte[] FileContent { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
    }
}
