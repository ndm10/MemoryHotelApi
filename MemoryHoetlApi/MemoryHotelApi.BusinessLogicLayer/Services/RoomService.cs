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
    public class RoomService : GenericService<Room>, IRoomService
    {
        public RoomService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<ResponseGetRoomDto> GetRoomAsync(Guid id)
        {
            var includes = new string[]
            {
                nameof(Room.RoomCategory),
                nameof(Room.Branch),
                nameof(Room.Conveniences),
                nameof(Room.Images)
            };

            // Get room by ID with includes
            var room = await _unitOfWork.RoomRepository!.GetByIdAsync(id, includes: includes);

            if (room == null)
            {
                return new ResponseGetRoomDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy phòng này!"
                };
            }

            // Map to response DTO
            var roomDto = _mapper.Map<RoomDto>(room);

            return new ResponseGetRoomDto
            {
                StatusCode = 200,
                Data = roomDto
            };
        }

        public async Task<ResponseGetRoomsDto> GetRoomsAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? branchId, Guid? roomCategoryId)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Get rooms with pagination and filtering
            var predicate = PredicateBuilder.New<Room>(x => !x.IsDeleted);

            if (textSearch != null)
            {
                predicate = predicate.And(x => x.Name.Contains(textSearch) || x.Description.Contains(textSearch));
            }

            if (status != null)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            if (branchId != null)
            {
                predicate = predicate.And(x => x.BranchId == branchId);
            }

            if (roomCategoryId != null)
            {
                predicate = predicate.And(x => x.RoomCategoryId == roomCategoryId);
            }

            var includes = new string[]
            {
                nameof(Room.RoomCategory),
                nameof(Room.Branch),
                nameof(Room.Conveniences),
                nameof(Room.Images)
            };

            var rooms = await _unitOfWork.RoomRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)rooms.Count() / pageSizeValue);

            // Map to response DTO
            return new ResponseGetRoomsDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<RoomDto>>(rooms),
                TotalPage = totalPages,
                TotalRecord = rooms.Count(),
            };
        }

        public async Task<BaseResponseDto> SoftDeleteRoomAsync(Guid id)
        {
            // Check if the room exists
            var room = await _unitOfWork.RoomRepository!.GetByIdAsync(id);

            if (room == null)
            {
                return new BaseResponseDto
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy phòng này!"
                };
            }

            // Soft delete the room
            room.IsDeleted = true;

            // Update the room in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa phòng thành công!"
            };
        }

        public async Task<BaseResponseDto> UpdateRoomAsync(RequestUpdateRoomDto request, Guid id)
        {
            var includes = new string[]
            {
                nameof(Room.RoomCategory),
                nameof(Room.Branch),
                nameof(Room.Conveniences),
                nameof(Room.Images)
            };

            // Get room by ID with includes
            var room = await _unitOfWork.RoomRepository!.GetByIdAsync(id, includes: includes);

            if (room == null)
            {
                return new ResponseGetRoomDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy phòng này!"
                };
            }

            if (request.RoomCategoryId.HasValue)
            {
                // Check if the room category exists
                var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(request.RoomCategoryId.Value);
                if (roomCategory == null)
                {
                    return new BaseResponseDto
                    {
                        IsSuccess = false,
                        Message = "Không tìm thấy hạng phòng này!"
                    };
                }

                room.RoomCategoryId = request.RoomCategoryId.Value;
            }

            if (request.BranchId.HasValue)
            {
                // Check if the branch exists
                var branchIncludes = new string[]
                {
                    nameof(Branch.RoomCategories),
                };

                var branch = await _unitOfWork.BranchRepository!.GetByIdAsync(request.BranchId.Value, includes: branchIncludes);

                // Check if the branch exists
                if (branch == null)
                {
                    return new BaseResponseDto
                    {
                        IsSuccess = false,
                        Message = "Không tìm thấy chi nhánh này!"
                    };
                }

                var roomCategory = new RoomCategory();

                if (request.RoomCategoryId.HasValue)
                {
                    roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(request.RoomCategoryId.Value);
                }
                else
                {
                    roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(room.RoomCategoryId);
                }

                // Automatically add the room category to the branch if category is not already associated with the branch
                if (!branch.RoomCategories.Contains(roomCategory!))
                {
                    branch.RoomCategories.Add(roomCategory!);
                }

                room.BranchId = request.BranchId.Value;
                room.Branch = branch;
            }

            if (request.ConvenienceIds != null && request.ConvenienceIds.Count > 0)
            {
                var conveniences = new List<Convenience>();
                foreach (var convenienceId in request.ConvenienceIds)
                {
                    // Find the convenience by ID
                    var convenience = await _unitOfWork.ConvenienceRepository!.GetByIdAsync(convenienceId);

                    // Check if the convenience exists
                    if (convenience == null)
                    {
                        return new BaseResponseDto
                        {
                            IsSuccess = false,
                            Message = $"Không tìm thấy tiện nghi với ID {convenienceId}!"
                        };
                    }

                    // Add the convenience to the list
                    conveniences.Add(convenience);
                }

                // Set the new conveniences
                room.Conveniences = conveniences;
            }

            if (request.ImageUrls != null && request.ImageUrls.Count > 0)
            {
                var images = new List<Image>();
                foreach (var imageUrl in request.ImageUrls)
                {
                    // Find the image by URL
                    var image = await _unitOfWork.ImageRepository!.GetImageByUrlAsync(imageUrl);
                    // Check if the image exists
                    if (image == null)
                    {
                        return new BaseResponseDto
                        {
                            IsSuccess = false,
                            Message = $"Không tìm thấy hình ảnh với URL {imageUrl}!"
                        };
                    }
                    // Add the image to the list
                    images.Add(image);
                }
                // Set the new images
                room.Images = images;
            }

            // Update other properties
            room.Name = request.Name ?? room.Name;
            room.Description = request.Description ?? room.Description;
            room.Area = request.Area ?? room.Area;
            room.BedType = request.BedType ?? room.BedType;
            room.Capacity = request.Capacity ?? room.Capacity;
            room.PricePerNight = request.PricePerNight ?? room.PricePerNight;
            room.IsActive = request.IsActive ?? room.IsActive;

            // Update the room in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật phòng thành công!"
            };
        }

        public async Task<BaseResponseDto> UploadRoomAsync(RequestUploadRoomDto request)
        {
            // Check if the room category exists
            var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(request.RoomCategoryId);
            if (roomCategory == null)
            {
                return new BaseResponseDto
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy hạng phòng này!"
                };
            }

            // Check if the branch exists
            var branchIncludes = new string[]
            {
                nameof(Branch.RoomCategories),
            };
            var branch = await _unitOfWork.BranchRepository!.GetByIdAsync(request.BranchId, includes: branchIncludes);
            if (branch == null)
            {
                return new BaseResponseDto
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy chi nhánh này!"
                };
            }

            // Automatically add the room category to the branch if category is not already associated with the branch
            if (!branch.RoomCategories.Contains(roomCategory))
            {
                branch.RoomCategories.Add(roomCategory);
            }

            // Check if the conveniences exist
            var conveniences = new List<Convenience>();
            foreach (var convenienceId in request.ConvenienceIds)
            {
                // Find the convenience by ID
                var convenience = await _unitOfWork.ConvenienceRepository!.GetByIdAsync(convenienceId);

                // Check if the convenience exists
                if (convenience == null)
                {
                    return new BaseResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Không tìm thấy tiện nghi với ID {convenienceId}!"
                    };
                }

                // Add the convenience to the list
                conveniences.Add(convenience);
            }

            // Check if the images exist
            var images = new List<Image>();
            foreach (var imageUrl in request.ImageUrls)
            {
                // Find the image by URL
                var image = await _unitOfWork.ImageRepository!.GetImageByUrlAsync(imageUrl);
                // Check if the image exists
                if (image == null)
                {
                    return new BaseResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Không tìm thấy hình ảnh với URL {imageUrl}!"
                    };
                }
                // Add the image to the list
                images.Add(image);
            }

            // Mapping to Room entity
            var room = _mapper.Map<Room>(request);

            // Set Conveniences and Images
            room.Conveniences = conveniences;
            room.Images = images;

            // Add the room to the database
            await _unitOfWork.RoomRepository!.AddAsync(room);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm phòng thành công!"
            };
        }
    }
}
