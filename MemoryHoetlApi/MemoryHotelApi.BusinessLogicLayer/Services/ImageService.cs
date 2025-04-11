using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ImageDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.Repositories.Interface;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class ImageService : GenericService<Image>, IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HashSet<string> _allowedExtensions = new HashSet<string> { ".png", ".jpg", ".jpeg" };

        public ImageService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ImageUploadResponseDto> UploadImage(List<ImageUploadRequestDto> imageDtos, string localRootPath, string urlPath)
        {
            if (imageDtos == null || imageDtos.Count == 0)
            {
                return new ImageUploadResponseDto()
                {
                    Message = "No files were uploaded."
                };
            }

            var filePaths = new List<string>();

            foreach (var imageDto in imageDtos)
            {
                var validateDto = ValidateImage(imageDto);
                if (validateDto == null)
                {
                    string fileName = $"{Guid.NewGuid()}{imageDto.FileExtension}";
                    await SaveImageAsync(imageDto.FileContent, fileName, localRootPath);

                    var filePath = Path.Combine(urlPath, fileName);

                    var imageEntity = new Image
                    {
                        Url = filePath,
                    };

                    await _unitOfWork.ImageRepository!.AddImageAsync(imageEntity);
                    await _unitOfWork.SaveChangesAsync();
                    filePaths.Add(filePath);
                }
                else
                {
                    return validateDto;
                }
            }

            return new ImageUploadResponseDto()
            {
                Urls = filePaths
            };
        }

        private ImageUploadResponseDto? ValidateImage(ImageUploadRequestDto imageDto)
        {
            string fileExtension = Path.GetExtension(imageDto.FileName).ToLower();

            if (imageDto == null || imageDto.FileContent == null || imageDto.FileContent.Length == 0)
            {
                return new ImageUploadResponseDto()
                {
                    Message = "File content is empty or invalid."
                };
            }
            else if (!_allowedExtensions.Contains(fileExtension))
            {
                return new ImageUploadResponseDto()
                {
                    Message = $"Invalid file extension for {imageDto.FileName}. Only {string.Join(", ", _allowedExtensions)} are allowed."
                };
            }
            else if (!IsValidImageHeader(imageDto.FileContent, fileExtension))
            {
                return new ImageUploadResponseDto()
                {
                    Message = $"File {imageDto.FileName} is not a valid image based on header."
                };
            }

            return null;
        }

        private bool IsValidImageHeader(byte[] fileContent, string extension)
        {
            if (fileContent.Length < 8) return false;

            if (extension == ".png")
            {
                return fileContent[0] == 0x89 && fileContent[1] == 0x50 && fileContent[2] == 0x4E && fileContent[3] == 0x47;
            }

            if (extension == ".jpg" || extension == ".jpeg")
            {
                return fileContent[0] == 0xFF && fileContent[1] == 0xD8;
            }

            return false;
        }

        private async Task SaveImageAsync(byte[] fileContent, string fileName, string localRootPath)
        {
            if (!Directory.Exists(localRootPath))
            {
                Directory.CreateDirectory(localRootPath);
            }

            string localFilePath = Path.Combine(localRootPath, fileName);
            await File.WriteAllBytesAsync(localFilePath, fileContent);
        }
    }
}
