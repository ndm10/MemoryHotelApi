using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ImageDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ImageDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _imageService = imageService;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<ImageUploadResponseDto>> UploadImage([FromForm] IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files were uploaded.");

            var imageDtos = new List<ImageUploadRequestDto>();

            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (file.Length > 0)
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileContent = memoryStream.ToArray();
                        imageDtos.Add(new ImageUploadRequestDto
                        {
                            FileName = file.FileName,
                            FileContent = fileContent,
                            FileExtension = Path.GetExtension(file.FileName)
                        });
                    }
                }
            }

            var localRootPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            var urlPath = "Images";
            var response = await _imageService.UploadImage(imageDtos, localRootPath, urlPath);
            return Ok(response);
        }
    }
}
