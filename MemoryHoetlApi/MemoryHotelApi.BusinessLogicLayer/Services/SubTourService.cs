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
    public class SubTourService : ISubTourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubTourService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetSubTourDto> GetSubTourAsync(Guid id)
        {
            // Find the sub tour bu ID
            string[] include =
            {
                nameof(SubTour.Images),
                nameof(SubTour.Tour),
            };

            var subTour = await _unitOfWork.SubTourRepository!.GetByIdIncludeAsync(id, include);

            if (subTour == null)
            {
                return new ResponseGetSubTourDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy tour này!"
                };
            }
            return new ResponseGetSubTourDto
            {
                StatusCode = 200,
                Data = _mapper.Map<GetSubTourDto>(subTour),
                IsSuccess = true,
            };
        }

        public async Task<ResponseGetSubToursDto> GetSubToursAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status, Guid? tourId)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            // Create predicate
            var predicate = PredicateBuilder.New<SubTour>(x => !x.IsDeleted);

            // Check if the textSearch parameter is not null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                // Add the text search condition to the predicate
                predicate = predicate.And(x => x.Title.Contains(textSearch) || x.Description.Contains(textSearch));
            }

            // Check if the status parameter is not null
            if (status.HasValue)
            {
                // Add the status condition to the predicate
                predicate = predicate.And(x => x.IsActive == status.Value);
            }

            // Check if the tourId parameter is not null
            if (tourId.HasValue)
            {
                predicate = predicate.And(x => x.TourId == tourId);
            }

            string[] includes =
            {
                nameof(SubTour.Images),
                nameof(SubTour.Tour),
            };

            // Get the subTours
            var subTours = await _unitOfWork.SubTourRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate, includes);

            // Get the total count of subTours
            var totalRecords = await _unitOfWork.SubTourRepository!.CountEntities(predicate);

            return new ResponseGetSubToursDto
            {
                StatusCode = 200,
                Data = _mapper.Map<List<GetSubTourDto>>(subTours.OrderBy(x => x.Order)),
                IsSuccess = true,
                TotalRecord = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSizeValue)
            };
        }

        public async Task<BaseResponseDto> SoftDeleteSubTourAsync(Guid id)
        {
            // Find the sub tour by ID
            var subTour = await _unitOfWork.SubTourRepository!.GetByIdAsync(id);

            // If sub tour is not found
            if (subTour == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Không tìm thấy tour này!"
                };
            }

            // Soft delete the sub tour
            subTour.IsDeleted = true;

            // Update the sub tour in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa tour thành công!"
            };
        }

        public async Task<BaseResponseDto> UpdateSubTourAsync(RequestUpdateSubTourDto request, Guid id)
        {
            var includes = new string[] {
                nameof(SubTour.Images),
            };

            // Find tour by the id
            var subTour = await _unitOfWork.SubTourRepository!.GetByIdIncludeAsync(id, includes);

            // If tour is not found
            if (subTour is null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Không tìm thấy tour du lịch này",
                    IsSuccess = false,
                };
            }

            // If tour is found, update the tour
            subTour.Title = request.Title ?? subTour.Title;
            subTour.DepartureTime = request.DepartureTime ?? subTour.DepartureTime;
            subTour.Duration = request.Duration ?? subTour.Duration;
            subTour.Transportation = request.Transportation ?? subTour.Transportation;
            subTour.TravelSchedule = request.TravelSchedule ?? subTour.TravelSchedule;
            subTour.Description = request.Description ?? subTour.Description;
            subTour.Price = request.Price ?? subTour.Price;
            subTour.TourId = request.TourId ?? subTour.TourId;
            subTour.IsActive = request.IsActive ?? subTour.IsActive;
            subTour.Order = request.Order ?? subTour.Order;

            // Find the image by url
            if (request.ImageUrls != null)
            {
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
                subTour.Images = images;
            }

            // Update tour to database
            _unitOfWork.SubTourRepository.Update(subTour);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Cập nhật tour thành công",
                IsSuccess = true,
            };
        }

        public async Task<BaseResponseDto> UploadSubTourAsync(RequestUploadSubTourDto request)
        {
            var predicate = PredicateBuilder.New<SubTour>(x => !x.IsDeleted);

            var subTours = await _unitOfWork.SubTourRepository!.GetAllAsync(predicate);
            var orders = subTours.Select(x => x.Order).ToList();
            var maxOrder = orders.Count() > 0 ? orders.Max() : 0;

            // Check if the Order is null or not
            if (!request.Order.HasValue || !request.Order.HasValue || request.Order == 0)
            {
                // If Order is null, set it to the maximum value in the database
                maxOrder = maxOrder + 1;
                request.Order = maxOrder;
            }

            // Mapping data to tour entity
            var subTour = _mapper.Map<SubTour>(request);

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
                subTour.Images.Add(image);
            }

            subTour.IsActive = true;

            // Add tour to database
            await _unitOfWork.SubTourRepository!.AddAsync(subTour);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Tải lên tour thành công",
                IsSuccess = true,
            };
        }
    }
}
