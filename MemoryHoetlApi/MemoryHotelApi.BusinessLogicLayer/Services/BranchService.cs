using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.BusinessLogicLayer.Utilities;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StringUtility _stringUtility;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper, StringUtility stringUtility)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stringUtility = stringUtility;
        }

        public async Task<ResponseGetBranchDto> GetBranchAsync(Guid id)
        {
            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted);

            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetBranchByIdAsync(id, predicate);

            if (branch == null)
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
                Data = _mapper.Map<BranchDto>(branch)
            };
        }

        public async Task<ResponseGetBranchesDto> GetBranchesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
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
            var branches = await _unitOfWork.BranchRepository!.GetBranchPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)branches.Count() / pageSizeValue);

            // Count the total records
            var totalRecords = await _unitOfWork.BranchRepository!.CountEntities(predicate);

            return new ResponseGetBranchesDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<BranchDto>>(branches.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<ResponseGetBranchesExploreDto> GetBranchesExploreAsync()
        {
            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted && x.IsActive);

            // Get all the branches from the database
            var branches = await _unitOfWork.BranchRepository!.GetAllBranchesAsync();

            // Map the branches to the DTO and return
            return new ResponseGetBranchesExploreDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = _mapper.Map<List<GetBranchesExploreDto>>(branches.OrderBy(x => x.Order))
            };
        }

        public async Task<ResponseGetBranchExploreDto> GetBranchExploreAsync(Guid id)
        {
            // Create a predicate for filtering
            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted && x.IsActive);

            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetBranchByIdAsync(id, predicate);

            if (branch == null || branch.IsDeleted)
            {
                return new ResponseGetBranchExploreDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy chi nhánh này",
                };
            }

            return new ResponseGetBranchExploreDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Data = _mapper.Map<GetBranchesExploreDto>(branch)
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
            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted);

            // Find the branch by id
            var branch = await _unitOfWork.BranchRepository!.GetBranchByIdAsync(id, predicate);

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

            // Validate the conveniences exist in the Database or not
            if (request.GeneralConvenienceIDs != null && request.GeneralConvenienceIDs.Count > 0)
            {
                var conveniences = await _unitOfWork.ConvenienceRepository!.GetAllAsync(x => request.GeneralConvenienceIDs.Contains(x.Id));
                if (conveniences == null || conveniences.Count() == 0)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                        IsSuccess = false,
                    };
                }

                branch.GeneralConveniences.Clear();
                branch.GeneralConveniences = conveniences.ToList();
            }

            if (request.HighlightedConvenienceIDs != null && request.HighlightedConvenienceIDs.Count > 0)
            {
                var conveniences = await _unitOfWork.ConvenienceRepository!.GetAllAsync(x => request.HighlightedConvenienceIDs.Contains(x.Id));
                if (conveniences == null || conveniences.Count() == 0)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                        IsSuccess = false,
                    };
                }

                branch.HighlightedConveniences.Clear();
                branch.HighlightedConveniences = conveniences.ToList();
            }

            // Check images exist in the database
            if (request.ImageUrls != null && request.ImageUrls.Count > 0)
            {
                branch.BranchImages.Clear();
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

                // Create list of images
                var branchImages = new List<BranchImage>();
                var order = 1;
                foreach (var image in images)
                {
                    var branchImage = new BranchImage
                    {
                        BranchId = branch.Id,
                        ImageId = image.Id,
                        Image = image,
                        Branch = branch,
                        Order = order
                    };

                    branch.BranchImages.Add(branchImage);
                    order++;
                }
            }

            // Update the LocationExplore
            if (request.LocationExplores != null && request.LocationExplores.Count > 0)
            {
                var locationExplores = _mapper.Map<List<LocationExplore>>(request.LocationExplores);
                branch.LocationExplores.Clear();
                branch.LocationExplores = locationExplores;
            }

            // Check if room category Id exist in the Database
            if (request.RoomCategoryIDs != null && request.RoomCategoryIDs.Count > 0)
            {
                foreach (var roomCategoryId in request.RoomCategoryIDs)
                {
                    var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(roomCategoryId);
                    if (roomCategory == null || roomCategory.IsDeleted)
                    {
                        return new BaseResponseDto
                        {
                            StatusCode = 400,
                            Message = "ID hạng phòng không hợp lệ vui lòng thử lại",
                            IsSuccess = false,
                        };
                    }
                    branch.RoomCategories.Add(roomCategory);
                }

                // If all room category is valid, add to branch
                var roomCategories = await _unitOfWork.RoomCategoryRepository!.GetAllAsync(x => request.RoomCategoryIDs.Contains(x.Id));
                branch.RoomCategories = roomCategories.ToList();
            }

            // Map the request to the branch entity
            branch.Name = request.Name?.Trim().Replace("\\s+", " ") ?? branch.Name;
            branch.Address = request.Address?.Trim().Replace("\\s+", " ") ?? branch.Address;
            branch.LocationHighlights = request.LocationHighlights ?? branch.LocationHighlights;
            branch.BranchLocation = request.BranchLocation ?? branch.BranchLocation;
            branch.SuitableFor = request.SuitableFor ?? branch.SuitableFor;
            branch.PricePerNight = request.PricePerNight ?? branch.PricePerNight;
            branch.Description = request.Description ?? branch.Description;
            branch.Order = request.Order ?? branch.Order;
            branch.Slug = request.Slug ?? branch.Slug;
            branch.IsActive = request.IsActive ?? branch.IsActive;
            branch.HotelCode = request.HotelCode?.Trim() ?? branch.HotelCode;

            // Update the branch in the database
            _unitOfWork.BranchRepository.Update(branch);
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
            // Validate the conveniences exist in the Database or not
            var generalConveniences = await _unitOfWork.ConvenienceRepository!.GetAllAsync(x => request.GeneralConvenienceIDs.Contains(x.Id));
            if (generalConveniences == null || generalConveniences.Count() == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                    IsSuccess = false,
                };
            }

            var highlightedConveniences = await _unitOfWork.ConvenienceRepository!.GetAllAsync(x => request.HighlightedConvenienceIDs.Contains(x.Id));
            if (highlightedConveniences == null || highlightedConveniences.Count() == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "ID dịch vụ không hợp lệ vui lòng thử lại",
                    IsSuccess = false,
                };
            }

            var predicate = PredicateBuilder.New<Branch>(x => !x.IsDeleted);

            // Calculate the maximum order in the database
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

            var order = 1;
            // Add images to branch
            foreach (var image in images)
            {
                var branchImage = new BranchImage
                {
                    BranchId = branch.Id,
                    ImageId = image.Id,
                    Image = image,
                    Branch = branch,
                    Order = order
                };

                branch.BranchImages.Add(branchImage);
                order++;
            }

            // Add conveniences to branch
            branch.GeneralConveniences = generalConveniences.ToList();
            branch.HighlightedConveniences = highlightedConveniences.ToList();

            // Generate the slug if slug in the request is null
            if (string.IsNullOrEmpty(request.Slug))
            {
                var slug = _stringUtility.GenerateSlug(request.Name);

                // Check if the slug is unique
                var existingBranch = await _unitOfWork.BranchRepository!.GetBySlugAsync(slug);
                if (existingBranch != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "Slug đã tồn tại, vui lòng thử lại",
                        IsSuccess = false,
                    };
                }

                branch.Slug = slug;
            }
            else
            {
                // Check if the slug is unique
                var existingBranch = await _unitOfWork.BranchRepository!.GetBySlugAsync(request.Slug);
                if (existingBranch != null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 400,
                        Message = "Slug đã tồn tại, vui lòng thử lại",
                        IsSuccess = false,
                    };
                }
            }

            // Add location explores to branch
            if (request.LocationExplores != null && request.LocationExplores.Count > 0)
            {
                var locationExplores = _mapper.Map<List<LocationExplore>>(request.LocationExplores);
                branch.LocationExplores = locationExplores;
            }

            // Check if room category Id exist in the Database
            if (request.RoomCategoryIDs != null && request.RoomCategoryIDs.Count > 0)
            {
                foreach (var roomCategoryId in request.RoomCategoryIDs)
                {
                    var roomCategory = await _unitOfWork.RoomCategoryRepository!.GetByIdAsync(roomCategoryId);
                    if (roomCategory == null || roomCategory.IsDeleted)
                    {
                        return new BaseResponseDto
                        {
                            StatusCode = 400,
                            Message = "ID hạng phòng không hợp lệ vui lòng thử lại",
                            IsSuccess = false,
                        };
                    }
                    branch.RoomCategories.Add(roomCategory);
                }

                // If all room category is valid, add to branch
                var roomCategories = await _unitOfWork.RoomCategoryRepository!.GetAllAsync(x => request.RoomCategoryIDs.Contains(x.Id));
                branch.RoomCategories = roomCategories.ToList();
            }

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
