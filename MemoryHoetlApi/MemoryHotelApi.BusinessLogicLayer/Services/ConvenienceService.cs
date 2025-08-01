using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.AdminDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using System.Text.RegularExpressions;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class ConvenienceService : IConvenienceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConvenienceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseGetConveniencesDto> GetConveniencesAsync(int? pageIndex, int? pageSize, string? textSearch, bool? status)
        {
            var pageIndexValue = pageIndex ?? Constants.PageIndexDefault;
            var pageSizeValue = pageSize ?? Constants.PageSizeDefault;

            var predicate = PredicateBuilder.New<Convenience>(x => !x.IsDeleted);

            // check if textSearch is null or empty
            if (!string.IsNullOrEmpty(textSearch))
            {
                predicate = predicate.And(x => (x.Name != null && x.Name.Contains(textSearch, StringComparison.OrdinalIgnoreCase)));
            }

            // Check if status is null or empty
            if (status.HasValue)
            {
                predicate = predicate.And(x => x.IsActive == status);
            }

            // Get all the conveniences from the database
            var conveniences = await _unitOfWork.ConvenienceRepository!.GenericGetPaginationAsync(pageIndexValue, pageSizeValue, predicate);

            // Count the total records
            var totalRecords = await _unitOfWork.ConvenienceRepository!.CountEntities(predicate);

            // Calculate the total page
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSizeValue);

            return new ResponseGetConveniencesDto
            {
                StatusCode = 200,
                // Mapping to DTO and sort order
                Data = _mapper.Map<List<ConvenienceDto>>(conveniences.OrderBy(x => x.Order)),
                TotalPage = totalPages,
                TotalRecord = totalRecords
            };
        }

        public async Task<ResponseGetConvenienceDto> GetConvenienceAsync(Guid id)
        {
            // Find the convenience by id
            var convenience = await _unitOfWork.ConvenienceRepository!.GetByIdAsync(id);

            // Check if the convenience exists
            if (convenience == null || convenience.IsDeleted)
            {
                return new ResponseGetConvenienceDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                };
            }

            // Map the convenience to the response DTO
            var convenienceDto = _mapper.Map<ConvenienceDto>(convenience);

            return new ResponseGetConvenienceDto
            {
                StatusCode = 200,
                Data = convenienceDto,
                IsSuccess = true,
            };
        }

        public Task<BaseResponseDto> SoftDeleteConvenienceAsync(Guid id)
        {
            // Find the convenience by id
            var convenience = _unitOfWork.ConvenienceRepository!.GetByIdAsync(id).Result;
            // Check if the convenience exists
            if (convenience == null || convenience.IsDeleted)
            {
                return Task.FromResult(new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                });
            }

            // Soft delete the convenience
            convenience.IsDeleted = true;
            _unitOfWork.ConvenienceRepository.Update(convenience);
            _unitOfWork.SaveChangesAsync();

            return Task.FromResult(new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Xóa dịch vụ thành công.",
            });
        }

        public async Task<BaseResponseDto> UpdateConvenienceAsync(RequestUpdateConvenienceDto request, Guid id)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                // Format the name to remove extra spaces and uppercase each word
                request.Name = Regex.Replace(request.Name.Trim(), @"\s+", " ");
                request.Name = string.Join(" ", request.Name.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
            }

            // Find the convenience by id
            var convenience = await _unitOfWork.ConvenienceRepository!.GetByIdAsync(id);

            // Check if the convenience exists
            if (convenience == null || convenience.IsDeleted)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    IsSuccess = false,
                    Message = "Dịch vụ không tồn tại.",
                };
            }

            // Map the request to the convenience entity
            convenience.Name = request.Name ?? convenience.Name;
            convenience.Icon = request.Icon ?? convenience.Icon;
            convenience.Description = request.Description ?? convenience.Description;
            convenience.IsActive = request.IsActive ?? convenience.IsActive;
            convenience.Order = request.Order ?? convenience.Order;

            // Update the convenience in the database
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Cập nhật dịch vụ thành công.",
            };
        }

        public async Task<BaseResponseDto> UploadConvenienceAsync(RequestUploadConvenienceDto request)
        {
            // Format the name to remove extra spaces and uppercase each word
            request.Name = Regex.Replace(request.Name.Trim(), @"\s+", " ");
            request.Name = string.Join(" ", request.Name.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));

            var predicate = PredicateBuilder.New<Convenience>(x => !x.IsDeleted && x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            var conveniencesExist = await _unitOfWork.ConvenienceRepository!.GetAllAsync(predicate);

            if (conveniencesExist != null && conveniencesExist.Count() > 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    Message = "Dịch vụ đã tồn tại.",
                };
            }

            predicate = PredicateBuilder.New<Convenience>(x => !x.IsDeleted);
            var conveniences = await _unitOfWork.ConvenienceRepository!.GetAllAsync(predicate);
            
            int order = 1;
            if (conveniences != null && conveniences.Count() > 0)
            {
                order = conveniences.Max(x => x.Order) + 1;
            }

            var convenience = _mapper.Map<Convenience>(request);
            convenience.Order = order;
            convenience.IsActive = true;

            await _unitOfWork.ConvenienceRepository.AddAsync(convenience);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                IsSuccess = true,
                Message = "Thêm dịch vụ thành công.",
            };
        }
    }
}
