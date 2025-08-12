using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AccountDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MemoryHotelApi.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "BlogWriter")]
    public class BlogWriterController : ControllerBase
    {
        private readonly IBlogWriterService _blogWriterService;

        public BlogWriterController(IBlogWriterService blogWriterService)
        {
            _blogWriterService = blogWriterService;
        }

        [HttpGet("blog")]
        public async Task<ActionResult<GenericResponsePagination<BlogDto>>> GetBlogAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi xảy ra trong quá trình xác thực tài khoản!"
                });
            }


            var response = await _blogWriterService.GetBlogsAsync(pageIndex, pageSize, textSearch, status, Guid.Parse(userId));
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("blog/{id}")]
        public async Task<ActionResult<ResponseGetBlogDto>> GetBlogByIdAsync(Guid id)
        {
            var response = await _blogWriterService.GetBlogByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("blog")]
        public async Task<ActionResult<BaseResponseDto>> CreateBlogAsync([FromBody] RequestCreateBlogDto blogDto)
        {
            // Get the user ID from the JWT token
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest(new ResponseGetProfileDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Có lỗi xảy ra trong quá trình xác thực tài khoản!"
                });
            }

            var response = await _blogWriterService.CreateBlogAsync(blogDto, Guid.Parse(userId));
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("blog/{id}")]
        public async Task<ActionResult<BaseResponseDto>> UpdateBlogAsync(Guid id, [FromBody] RequestUpdateBlogDto blogDto)
        {
            var response = await _blogWriterService.UpdateBlogAsync(id, blogDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("blog/{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteBlogAsync(Guid id)
        {
            var response = await _blogWriterService.DeleteBlogAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
