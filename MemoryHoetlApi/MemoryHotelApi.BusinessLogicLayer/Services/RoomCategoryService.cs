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
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StringUtility _stringUtility;

        public RoomCategoryService(IUnitOfWork unitOfWork, IMapper mapper, StringUtility stringUtility)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stringUtility = stringUtility;
        }

        public async Task<ResponseGetRoomCategoriesDto> GetRoomCategoriesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<RoomCategory>(x => !x.IsDeleted);

            // Check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Name != null && x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the branches from the database
            var branches = await _unitOfWork.RoomCategoryRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total records
            var totalRecords = await _unitOfWork.RoomCategoryRepository!.CountEntitiesAsync(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new ResponseGetRoomCategoriesDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<RoomCategoryDto>>(branches.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<ResponseGetRoomCategoryDto> GetRoomCategoryAsync(Guid id)
        {
            // Find the room category by id
            var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(id);

            if (roomCategory == null)
            {
                return new ResponseGetRoomCategoryDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng phòng này"
                };
            }

            return new ResponseGetRoomCategoryDto
            {
                IsSuccess = true,
                StatusCode = 200,
                Data = _mapper.Map<RoomCategoryDto>(roomCategory)
            };
        }

        public async Task<BaseResponseDto> SoftDeleteRoomCategoryAsync(Guid id)
        {
            // Find the room category by id
            var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(id);

            if (roomCategory == null)
            {
                return new ResponseGetRoomCategoryDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng phòng này"
                };
            }

            roomCategory.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa hạng phòng thành công"
            };
        }

        public async Task<BaseResponseDto> UpdateRoomCategoryAsync(RequestUpdateRoomCategoryDto request, Guid id)
        {
            // Find the room category by id
            var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(id);

            // If not found
            if (roomCategory == null)
            {
                return new ResponseGetRoomCategoryDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy hạng phòng này"
                };
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                // Format the name of room category
                request.Name = _stringUtility.UpperFirstLetter(request.Name);
                // Check if the name exist or not
                var roomCategories = await _unitOfWork.RoomCategoryRepository!.GetAllAsync(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id && !x.IsDeleted);
                if (roomCategories.Any())
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        IsSuccess = false,
                        Message = "Hạng phòng đã tồn tại"
                    };
                }
                
                roomCategory.Name = request.Name;
            }

            // Map the request to the room category entity
            roomCategory.Order = request.Order ?? roomCategory.Order;
            roomCategory.IsActive = request.IsActive ?? roomCategory.IsActive;

            // Update the room category in the database
            await _unitOfWork.SaveChangesAsync();
            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật hạng phòng thành công"
            };
        }

        public async Task<BaseResponseDto> UploadRoomCategoryAsync(RequestUploadRoomCategoryDto request)
        {
            // Format the name of room category
            request.Name = _stringUtility.UpperFirstLetter(request.Name);

            // Check if the name exist or not
            var roomCategories = await _unitOfWork.RoomCategoryRepository!.GetAllAsync(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase) && !x.IsDeleted);

            if (roomCategories.Any())
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Hạng phòng đã tồn tại"
                };
            }

            var order = 1;
            roomCategories = await _unitOfWork.RoomCategoryRepository!.GetAllAsync();
            if (!request.Order.HasValue || request.Order.Value == 0)
            {
                order = roomCategories.Max(x => x.Order) + 1;
                request.Order = order;
            }

            // Mapping to entity and save to db
            var roomCategory = _mapper.Map<RoomCategory>(request);

            await _unitOfWork.RoomCategoryRepository!.AddAsync(roomCategory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm hạng phòng thành công"
            };
        }
    }
}
