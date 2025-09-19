using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class MotorcycleRentalHistoryService : GenericService<MotorcycleRentalHistory>, IMotorcycleRentalHistoryService
    {
        public MotorcycleRentalHistoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<BaseResponseDto> RequestCreateMotorcycleRentalAsync(RequestMotorcycleRentalDto request)
        {
            // Check if the branch is empty
            if (request.BranchId == Guid.Empty)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Please choose branch"
                };
            }

            // Check all items in the order is valid
            if (request.Items == null || request.Items.Count == 0)
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Please add at least one item to the order"
                };
            }

            // Check if the branch is exists
            var branch = await _unitOfWork.BranchRepository!.GetByIdAsync(request.BranchId);
            if (branch == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Branch not found"
                };
            }

            // Map the request to the entity
            var motorcycleRentalHistory = _mapper.Map<MotorcycleRentalHistory>(request);

            // Initialize default status for rental history (Pending)
            motorcycleRentalHistory.Status = 0;

            var orderBy = new Func<IQueryable<MotorcycleRentalHistory>, IOrderedQueryable<MotorcycleRentalHistory>>(q => q.OrderBy(m => m.RentalCode));

            // Get the last motorcycle rental history to set the order
            var lastMotorcycleRentalHistory = await _unitOfWork.MotorcycleRentalHistoryRepository!.GetLastEntityAsync(predicate: x => !x.IsDeleted, orderBy: orderBy);

            // If there is no last order, set the order code to "MR-01"
            if (lastMotorcycleRentalHistory == null)
            {
                motorcycleRentalHistory.RentalCode = "MR-01";
            }
            else
            {
                // Extract the numeric part from the last order code
                var lastOrderNumberPart = lastMotorcycleRentalHistory.RentalCode.Split('-').Last();
                if (int.TryParse(lastOrderNumberPart, out int lastOrderNumber))
                {
                    // Increment the numeric part and format it with leading zeros
                    var newOrderNumber = lastOrderNumber + 1;
                    motorcycleRentalHistory.RentalCode = $"MR-{newOrderNumber:D2}";
                }
                else
                {
                    // If parsing fails, fallback to a default value
                    motorcycleRentalHistory.RentalCode = "MR-01";
                }
            }

            // Check if all services is exists in the database and available
            foreach (var item in request.Items)
            {
                var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(item.Id);
                if (service == null || service.IsDeleted)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 404,
                        Message = $"Service with ID {item.Id} not found"
                    };
                }

                // Add the service to the rental history details
                motorcycleRentalHistory.Items.Add(
                    new MotorcycleRentalHistoryDetail
                    {
                        ServiceId = service.Id,
                        Name = service.Name,
                        Price = service.Price,
                        Image = service.Image,
                        Description = service.Description,
                        Quantity = item.Quantity,
                        TotalPrice = service.Price * item.Quantity,
                    });
            }

            // Add the rental history to the database
            await _unitOfWork.MotorcycleRentalHistoryRepository!.AddAsync(motorcycleRentalHistory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 201,
                Message = "Motorcycle rental request created successfully"
            };
        }
    }
}
