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
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetBranchDto> GetBranchAsync(Guid id)
        {
            var includes = new string[]
            {
                nameof(Branch.Images),
                nameof(Branch.Amenities)
            };

            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetByIdIncludeAsync(id, includes);

            if (branch == null || branch.IsDeleted)
            {
                return new ResponseGetBranchDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy chi nhánh này",
                };
            }

            return new ResponseGetBranchDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = _mapper.Map<GetBranchDto>(branch)
            };
        }

        public async Task<ResponseGetBranchesDto> GetBranchesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var includes = new string[]
            {
                nameof(Branch.Images),
                nameof(Branch.Amenities)
            };

            // Default values for pagination
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted);
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
            var branches = await _unitOfWork.BranchRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)branches.Count() / pageSizeValue);
            return new ResponseGetBranchesDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<GetBranchDto>>(branches.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = branches.Count()
            };
        }

        public async Task<BaseResponseDto> SoftDeleteBranchAsync(Guid id)
        {
            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetByIdAsync(id);

            // Check if the branch exists
            if (branch == null || branch.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Chi nhánh không tồn tại.",
                };
            }

            // Soft delete the branch
            branch.IsDeleted = true;
            _unitOfWork.BranchRepository.Update(branch);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa chi nhánh thành công.",
            };
        }

        public async Task<BaseResponseDto> UpdateBranchAsync(RequestUpdateBranchDto request, Guid id)
        {
            var includes = new string[]
            {
                nameof(Branch.Images),
                nameof(Branch.Amenities)
            };

            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetByIdIncludeAsync(id, includes);

            // Check if the branch exists
            if (branch == null || branch.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Chi nhánh không tồn tại.",
                };
            }

            // Validate the amenities exist in the Database or not
            if (request.AmenityIDs != null && request.AmenityIDs.Count > 0)
            {
                var amenities = await _unitOfWork.AmenityRepository!.GetAllAsync(x => request.AmenityIDs.Contains(x.Id));
                if (amenities == null || amenities.Count() == 0)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                        IsSuccess = false,
                    };
                }

                branch.Amenities.Clear();

                foreach (var amenity in amenities)
                {
                    branch.Amenities.Add(amenity);
                }
            }

            // Check images exist in the database
            if (request.ImageUrls != null && request.ImageUrls.Count > 0)
            {
                var images = await _unitOfWork.ImageRepository!.GetImagesWithCondition(x => request.ImageUrls.Contains(x.Url));
                if (images is null || images.Count == 0)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "Có lỗi với việc tả ảnh lên, vui lòng thử lại sau",
                        IsSuccess = false,
                    };

                }
                branch.Images = images.ToList();
            }

            // Map the request to the branch entity
            branch.Name = request.Name?.Trim().Replace("\\s+", " ") ?? branch.Name;
            branch.Address = request.Address?.Trim().Replace("\\s+", " ") ?? branch.Address;
            branch.LocationHighlights = request.LocationHighlights?.Trim().Replace("\\s+", " ") ?? branch.LocationHighlights;
            branch.SuitableFor = request.SuitableFor?.Trim().Replace("\\s+", " ") ?? branch.SuitableFor;
            branch.PricePerNight = request.PricePerNight ?? branch.PricePerNight;
            branch.Description = request.Description ?? branch.Description;
            branch.Order = request.Order ?? branch.Order;
            branch.IsActive = request.IsActive ?? branch.IsActive;

            // Update the branch in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật chi nhánh thành công.",
            };
        }

        public async Task<BaseResponseDto> UploadBranchAsync(RequestUploadBranchDto request)
        {
            // Validate the amenities exist in the Database or not
            var amenities = await _unitOfWork.AmenityRepository!.GetAllAsync(x => request.AmenityIDs.Contains(x.Id));
            if (amenities == null || amenities.Count() == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                    IsSuccess = false,
                };
            }

            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted);

            var branches = await _unitOfWork.BranchRepository!.GetAllAsync(predicate);
            var orders = branches.Select(x => x.Order).ToList();
            var maxOrder = orders.Count() > 0 ? orders.Max() : 1;

            // Check if the Order is null or not
            if (!request.Order.HasValue || !request.Order.HasValue || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data to tour entity
            var branch = _mapper.Map<Branch>(request);

            // Find the image by url
            var images = await _unitOfWork.ImageRepository!.GetImagesWithCondition(x => request.ImageUrls.Contains(x.Url));

            // Check if images is null
            if (images is null || images.Count == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Có lỗi với việc tả ảnh lên, vui lòng thử lại sau",
                    IsSuccess = false,
                };
            }

            // Add images to tour
            foreach (var image in images)
            {
                branch.Images.Add(image);
            }

            branch.Amenities = amenities.ToList();

            // Format the name and address
            branch.Name = branch.Name.Trim().Replace("\\s+", " ");
            branch.Address = branch.Address.Trim().Replace("\\s+", " ");

            // Add tour to database
            await _unitOfWork.BranchRepository!.AddAsync(branch);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Tải chi nhánh lên thành công",
                IsSuccess = true,
            };
        }
    }
}
