using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.BlogWriterDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class BlogWriterService : GenericService<Blog>, IBlogWriterService
    {
        private readonly StringUtility _stringUtility;
        private readonly EmailSender _emailSender;

        public BlogWriterService(IMapper mapper, IUnitOfWork unitOfWork, StringUtility stringUtility, EmailSender emailSender) : base(mapper, unitOfWork)
        {
            _stringUtility = stringUtility;
            _emailSender = emailSender;
        }

        public async Task<BaseResponseDto> CreateBlogAsync(RequestCreateBlogDto blogDto, Guid authorId)
        {
            // If order is null, then get the max order from the database and add 1 to it
            if (!blogDto.Order.HasValue)
            {
                var maxOrder = await _unitOfWork.BlogRepository!.GetMaxOrderAsync();
                blogDto.Order = maxOrder + 1;
            }

            // If slug is null, then generate a slug from the title
            if (string.IsNullOrEmpty(blogDto.Slug))
            {
                // Remove all icon, special characters from the title
                blogDto.Slug = _stringUtility.GenerateSlug(blogDto.Title!);
            }

            // Map the request DTO to the Blog entity
            var blog = _mapper.Map<Blog>(blogDto);

            // Find the author by Id
            var author = await _unitOfWork.UserRepository!.GetByIdAsync(authorId);

            if (author == null || author.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy tác giả!"
                };
            }

            // Set the AuthorId and IsActive properties
            blog.AuthorId = authorId;
            blog.Author = author;

            // Add hashtags to the blog
            if (blogDto.Hashtag != null && blogDto.Hashtag.Count > 0)
            {
                foreach (var hashtag in blogDto.Hashtag)
                {
                    // Format name to remove special characters and extra spaces
                    var hashtagFormater = _stringUtility.FormatHashtagName(hashtag);

                    // Check if the hashtag already exists in the database
                    var existingHashtag = await _unitOfWork.HashtagRepository!.GetEntityWithConditionAsync(x => x.Name == hashtagFormater);

                    // If the hashtag does not exist, create a new Hashtag entity
                    if (existingHashtag == null)
                    {
                        existingHashtag = new Hashtag
                        {
                            Name = hashtagFormater
                        };

                        await _unitOfWork.HashtagRepository.AddAsync(existingHashtag);
                    }

                    // Create a new BlogHashTag entity and add it to the BlogHashtags collection
                    blog.BlogHashtags.Add(new BlogHashTag
                    {
                        Hashtag = existingHashtag,
                        Blog = blog
                    });
                }
            }

            // Save the blog to the database
            await _unitOfWork.BlogRepository!.AddAsync(blog);
            await _unitOfWork.SaveChangesAsync();

            // Check if email notification is enabled
            if (blogDto.IsEmailNotification.HasValue && blogDto.IsEmailNotification.Value)
            {
                // Get all users who role is User
                var users = await _unitOfWork.UserRepository!.GetAllAsync(x => x.Role.Name.Equals(Constants.RoleUserName) && x.IsVerified && x.IsActive && !x.IsDeleted);

                // Send email notification to all users
                await _emailSender.SendEmailForBlogNotification(users.Select(x => x.Email).ToList(), blog.Title, $"{blog.Slug}_{blog.Id}");
            }

            return new BaseResponseDto
            {
                StatusCode = 201,
                IsSuccess = true,
                Message = "Tạo bài viết thành công!"
            };
        }

        public async Task<BaseResponseDto> DeleteBlogAsync(Guid id)
        {
            // Find the blog by id
            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(id, x => !x.IsDeleted);

            if (blog == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Soft delete the blog by setting IsDeleted to true
            blog.IsDeleted = true;

            _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa bài viết thành công!"
            };
        }

        public async Task<ResponseAdminGetBlogDto> GetBlogByIdAdminAsync(Guid id)
        {
            // Find the blog by id with includes for Author
            var includes = new[] { nameof(Blog.Author), nameof(Blog.BlogHashtags) };

            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(id, x => !x.IsDeleted);

            if (blog == null)
            {
                return new ResponseAdminGetBlogDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Map the blog to the response DTO
            var blogAdminDto = _mapper.Map<AdminBlogDto>(blog);

            return new ResponseAdminGetBlogDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = blogAdminDto
            };
        }

        public async Task<ResponseGetBlogDto> GetBlogByIdAsync(Guid id)
        {
            // Find the blog by id
            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(id, x => !x.IsDeleted);

            if (blog == null)
            {
                return new ResponseGetBlogDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Map the blog to the response DTO
            var blogDto = _mapper.Map<BlogDto>(blog);

            return new ResponseGetBlogDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = blogDto
            };
        }

        public async Task<ResponseGetBlogExploreDto> GetBlogExploreAsync(Guid id)
        {
            // Find the blog by id with includes for Author
            var includes = new[] { nameof(Blog.Author), nameof(Blog.BlogHashtags) };
            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(id, x => !x.IsDeleted && x.IsActive);

            if (blog == null)
            {
                return new ResponseGetBlogExploreDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Map the blog to the response DTO
            var blogExploreDto = _mapper.Map<BlogExploreDto>(blog);

            return new ResponseGetBlogExploreDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = blogExploreDto
            };
        }

        public async Task<GenericResponsePagination<AdminBlogDto>> GetBlogsAdminAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            // Set default values for pageIndex and pageSize if they are null
            var pageIndexValue = pageIndex ?? 1;
            var pageSizeValue = pageSize ?? 10;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Blog>(x => !x.IsDeleted);
            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Title != null && x.Title.Contains(textSearch)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the blogs from the database
            var blogs = await _unitOfWork.BlogRepository!.GetBlogsPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total number of records
            var totalRecords = await _unitOfWork.BlogRepository!.CountEntitiesAsync(predicate);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new GenericResponsePagination<AdminBlogDto>
            {
                StatusCode = 200,
                Data = _mapper.Map<List<AdminBlogDto>>(blogs),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<GenericResponsePagination<BlogDto>> GetBlogsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid authorId)
        {
            var pageIndexValue = pageIndex ?? 1;
            var pageSizeValue = pageSize ?? 10;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Blog>(x => !x.IsDeleted && x.AuthorId == authorId);
            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Title != null && x.Title.Contains(textSearch)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the blogs from the database
            var blogs = await _unitOfWork.BlogRepository!.GetBlogsPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total number of records
            var totalRecords = await _unitOfWork.BlogRepository!.CountEntitiesAsync(predicate);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new GenericResponsePagination<BlogDto>
            {
                StatusCode = 200,
                Data = _mapper.Map<List<BlogDto>>(blogs),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<GenericResponsePagination<BlogExploreDto>> GetBlogsExploreAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? 1;
            var pageSizeValue = pageSize ?? 10;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Blog>(x => !x.IsDeleted && x.IsActive);
            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Title != null && x.Title.Contains(textSearch)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            var includes = new[]
            {
                nameof(Blog.Author),
            };

            // Get all the blogs from the database
            var blogs = await _unitOfWork.BlogRepository!.GetBlogsPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total number of records
            var totalRecords = await _unitOfWork.BlogRepository!.CountEntitiesAsync(predicate);

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new GenericResponsePagination<BlogExploreDto>
            {
                StatusCode = 200,
                Data = _mapper.Map<List<BlogExploreDto>>(blogs),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<BaseResponseDto> UpdateBlogAsync(Guid id, RequestUpdateBlogDto blogDto)
        {
            // Find the blog by id
            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(id, x => !x.IsDeleted);

            if (blog == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Map the request DTO to the Blog entity
            blog.Title = blogDto.Title ?? blog.Title;
            blog.Content = blogDto.Content ?? blog.Content;
            blog.Thumbnail = blogDto.Thumbnail ?? blog.Thumbnail;
            blog.Description = blogDto.Description ?? blog.Description;
            blog.Order = blogDto.Order ?? blog.Order;
            blog.Location = blogDto.Location ?? blog.Location;
            blog.IsPopular = blogDto.IsPopular ?? blog.IsPopular;

            // Check if the hashtags are provided
            if (blogDto.Hashtag != null && blogDto.Hashtag.Count > 0)
            {
                // Clear existing hashtags
                blog.BlogHashtags.Clear();
                foreach (var hashtag in blogDto.Hashtag)
                {
                    // Format name to remove special characters and extra spaces
                    var hashtagFormater = _stringUtility.FormatHashtagName(hashtag);
                    // Check if the hashtag already exists in the database
                    var existingHashtag = await _unitOfWork.HashtagRepository!.GetEntityWithConditionAsync(x => x.Name == hashtagFormater);
                    // If the hashtag does not exist, create a new Hashtag entity
                    if (existingHashtag == null)
                    {
                        existingHashtag = new Hashtag
                        {
                            Name = hashtagFormater
                        };
                        await _unitOfWork.HashtagRepository.AddAsync(existingHashtag);
                    }
                    // Create a new BlogHashTag entity and add it to the BlogHashtags collection
                    blog.BlogHashtags.Add(new BlogHashTag
                    {
                        Hashtag = existingHashtag,
                        Blog = blog
                    });
                }
            }

            // If slug is null, then generate a slug from the title
            if (string.IsNullOrEmpty(blogDto.Slug))
            {
                // Remove all icon, special characters from the title
                blogDto.Slug = _stringUtility.GenerateSlug(blog.Title);
            }
            blog.Slug = blogDto.Slug;

            blog.MinutesToRead = blogDto.MinutesToRead ?? blog.MinutesToRead;
            blog.IsActive = blogDto.IsActive ?? blog.IsActive;

            // Update the blog in the database
            _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật bài viết thành công!"
            };
        }

        public async Task<BaseResponseDto> UpdateBlogByAdminAsync(Guid blogId, RequestAdminUpdateBlogDto blogDto)
        {
            // Find the blog by id
            var blog = await _unitOfWork.BlogRepository!.GetBlogByIdAsync(blogId, x => !x.IsDeleted);

            if (blog == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy bài viết này!"
                };
            }

            // Map the request DTO to the Blog entity
            blog.IsActive = blogDto.IsActive ?? blog.IsActive;

            // Update the blog in the database
            _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật bài viết thành công!"
            };
        }
    }
}
