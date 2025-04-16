using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class StoryService : IStoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetStoriesDto> GetStoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<Story>(x => !x.IsDeleted);

            // check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => x.Description != null && x.Description.Contains(textSearch));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the banners from the database
            var stories = await _unitOfWork.IStoryRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)stories.Count() / pageSizeValue);

            // Map the banners to the response DTO
            return new ResponseGetStoriesDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<GetStoryDto>>(stories),
                TotalPage = totalPages,
                TotalRecord = stories.Count(),
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetStoryDto> GetStoryAsync(Guid id)
        {
            var story = await _unitOfWork.IStoryRepository!.GetByIdAsync(id);

            if (story == null || story.IsDeleted)
            {
                return new ResponseGetStoryDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Story không tồn tại.",
                };
            }

            // Map the banner to the response DTO
            var storyDto = _mapper.Map<GetStoryDto>(story);

            return new ResponseGetStoryDto
            {
                StatusCode = 200,
                Data = storyDto,
                IsSuccess = true,
            };
        }

        public async Task<GenericResponseDto> SoftDeleteStoryAsync(Guid id)
        {
            var story = await _unitOfWork.IStoryRepository!.GetByIdAsync(id);

            if (story == null || story.IsDeleted)
            {
                return new GenericResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Story không tồn tại.",
                };
            }

            // Soft delete the banner
            story.IsDeleted = true;

            // Update the banner in the database
            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa story thành công.",
            };
        }

        public async Task<GenericResponseDto> UpdateStoryAsync(RequestUpdateStoryDto request, Guid id)
        {
            // Find the banner by ID
            var story = await _unitOfWork.IStoryRepository!.GetByIdAsync(id);

            // Check if the banner exists
            if (story == null || story.IsDeleted)
            {
                return new GenericResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Story không tồn tại.",
                };
            }

            // Map the request data to the banner entity
            story.ImageUrl = request.ImageUrl ?? story.ImageUrl;
            story.Link = request.Link ?? story.Link;
            story.Description = request.Description ?? story.Description;
            story.Order = request.Order ?? story.Order;
            story.IsActive = request.IsActive ?? story.IsActive;

            // Save the changes to the database
            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật banner thành công.",
            };
        }

        public async Task<GenericResponseDto> UploadStoryAsync(RequestUploadStoryDto request)
        {
            if (request == null)
            {
                return new GenericResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Không có story nào được tải lên.",
                };
            }

            var maxOrder = await _unitOfWork.IStoryRepository!.GetMaxOrder();

            // Check if the Order is null or not
            if (!request.Order.HasValue || !request.Order.HasValue || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data from request to entity
            var story = _mapper.Map<Story>(request);
            story.IsActive = true;

            // Save the banner to the database
            await _unitOfWork.IStoryRepository!.AddAsync(story);

            await _unitOfWork.SaveChangesAsync();

            return new GenericResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Tải story lên thành công.",
            };
        }
    }
}
