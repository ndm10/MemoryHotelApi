using AutoMapper;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ExploreDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class CarBookingHistoryService : GenericService<CarBookingHistory>, ICarBookingHistoryService
    {
        public CarBookingHistoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<BaseResponseDto> RequestCreateCarBookingAsync(RequestCarBookingDto request)
        {
            // Mapping request to entity
            var carBookingHistory = _mapper.Map<CarBookingHistory>(request);

            // Initialize default status for rental history (Pending)
            carBookingHistory.Status = 0;

            var orderBy = new Func<IQueryable<CarBookingHistory>, IOrderedQueryable<CarBookingHistory>>(q => q.OrderBy(m => m.BookingCode));

            // Get the last car booking history to set the order
            var lastCarBookingHistory = await _unitOfWork.CarBookingHistoryRepository!.GetLastEntityAsync(predicate: x => !x.IsDeleted, orderBy: orderBy);

            // If there is no last order, set the order code to "CB-01"
            if (lastCarBookingHistory == null)
            {
                carBookingHistory.BookingCode = "CB-01";
            }
            else
            {
                // Extract the numeric part of the last order code and increment it by 1
                var lastOrderNumber = int.Parse(lastCarBookingHistory.BookingCode.Split('-')[1]);
                var newOrderNumber = lastOrderNumber + 1;
                // Set the new order code with leading zeros (e.g., "CB-02", "CB-10")
                carBookingHistory.BookingCode = $"CB-{newOrderNumber:D2}";
            }

            // Check if all services is exists in the database and available
            foreach (var item in request.Items)
            {
                // Check if the service is exists
                var service = await _unitOfWork.ServiceRepository!.GetByIdAsync(item.Id);
                if (service == null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 404,
                        Message = "Car not found"
                    };
                }

                // Add the service to the order details
                carBookingHistory.Items.Add(new CarBookingHistoryDetail
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

            // Add the car booking history to the database
            await _unitOfWork.CarBookingHistoryRepository!.AddAsync(carBookingHistory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 201,
                Message = "Car booking created successfully"
            };
        }
    }
}
