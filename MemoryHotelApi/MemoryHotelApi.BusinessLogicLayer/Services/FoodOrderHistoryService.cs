using AutoMapper;
using LinqKit;
using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.Enums;
using MemoryHotelApi.BusinessLogicLayer.Common.ResponseDTOs;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.Receptionist;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.Receptionist;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using System;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class FoodOrderHistoryService : GenericService<FoodOrderHistory>, IFoodOrderHistoryService
    {
        public FoodOrderHistoryService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<BaseResponseDto> CreateOrderAsync(RequestCreateOrderDto request, Guid userId)
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

            // Check if the user is allowed to create an order in this branch
            var includes = new[]
            {
                nameof(User.BranchReceptionists),
                nameof(User.Role)
            };
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(userId, includes);

            // If user is Admin, allow to create order in any branch
            if (user == null || (user.Role.Name != Constants.RoleAdminName && !user.BranchReceptionists.Any(br => br.BranchId == request.BranchId)))
            {
                return new BaseResponseDto
                {
                    StatusCode = 403,
                    Message = "You are not allowed to create an order in this branch"
                };
            }

            // Map the request to the FoodOrderHistory entity
            var foodOrderHistory = new FoodOrderHistory();

            if (Enum.TryParse<FoodOrderStatus>(request.Status, ignoreCase: true, out var status))
            {
                foodOrderHistory.Status = (int)status;
            }
            else
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Invalid status"
                };
            }

            foodOrderHistory.Id = Guid.NewGuid();
            foodOrderHistory.BranchId = request.BranchId;
            foodOrderHistory.Room = request.Room;
            foodOrderHistory.CustomerPhone = request.CustomerPhone;
            foodOrderHistory.Note = request.Note;
            foodOrderHistory.CustomerName = request.CustomerName;
            foodOrderHistory.ReceptionistName = request.ReceptionistName;

            // Generate order code base on the last order code in the branch
            var lastOrder = await _unitOfWork.FoodOrderHistoryRepository!
                .GetLastOrderByBranchIdAsync(request.BranchId);

            // If there is no last order, set the order code to "ORDER-01"
            if (lastOrder == null)
            {
                foodOrderHistory.OrderCode = "ORDER-01";
            }
            else
            {
                // Extract the number from the last order code and increment it
                var lastOrderNumber = int.Parse(lastOrder.OrderCode.Split('-')[1]);
                foodOrderHistory.OrderCode = $"ORDER-{lastOrderNumber + 1:D2}";
            }

            // Check if all food is exists in the database and available
            foreach (var item in request.Items)
            {
                var food = await _unitOfWork.FoodRepository!.GetByIdAsync(item.Id);
                if (food == null)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 404,
                        Message = $"Food with ID {item.Id} not found"
                    };
                }

                // Add the food item to the order details
                foodOrderHistory.Items.Add(new FoodOrderHistoryDetail
                {
                    FoodId = item.Id,
                    Food = food,
                    Name = food.Name,
                    Price = food.Price,
                    Image = food.Image,
                    Description = food.Description,
                    Quantity = item.quantity,
                    TotalPrice = food.Price * item.quantity,
                });
            }

            // Add the order to the database
            await _unitOfWork.FoodOrderHistoryRepository!.AddAsync(foodOrderHistory);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                StatusCode = 200,
                Message = "Order created successfully"
            };
        }

        public async Task<ResponseGetFoodOrderHistoriesDto> GetFoodOrderHistoriesAsync(int? pageIndex, int? pageSize, string? textSearch, string? orderStatus, Guid receptionistId)
        {
            // Check the receptionist belong to which branch
            var includes = new[]
            {
                nameof(User.BranchReceptionists),
                nameof(User.Role)
            };

            var user = await _unitOfWork.UserRepository!.GetByIdAsync(receptionistId, includes);

            if (user == null)
            {
                return new ResponseGetFoodOrderHistoriesDto
                {
                    StatusCode = 404,
                    Message = "User not found"
                };
            }

            // Create predicates for filtering
            var predicates = PredicateBuilder.New<FoodOrderHistory>(x => !x.IsDeleted);

            // If user is not Admin, filter by branch
            if (user.Role.Name != Constants.RoleAdminName)
            {
                var branchId = user.BranchReceptionists
                    .Select(br => br.BranchId)
                    .FirstOrDefault();

                if (branchId == Guid.Empty)
                {
                    return new ResponseGetFoodOrderHistoriesDto
                    {
                        StatusCode = 403,
                        Message = "You are not allowed to view orders in this branch"
                    };
                }
                predicates = predicates.And(x => x.BranchId == branchId);
            }

            if (!string.IsNullOrEmpty(textSearch))
            {
                predicates = predicates.And(x => (!string.IsNullOrEmpty(x.CustomerPhone) && x.CustomerPhone.Contains(textSearch)) || x.Room.Contains(textSearch) || (x.Note != null && x.Note.Contains(textSearch)) || x.OrderCode.Contains(textSearch) || x.CustomerName.Contains(textSearch) || (!string.IsNullOrEmpty(x.ReceptionistName) && x.ReceptionistName.Contains(textSearch)) || x.Items.Any(i => i.Name.Contains(textSearch) || (!string.IsNullOrEmpty(i.Description) && i.Description.Contains(textSearch))));
            }

            if (!string.IsNullOrEmpty(orderStatus))
            {
                if (Enum.TryParse<FoodOrderStatus>(orderStatus, ignoreCase: true, out var status))
                {
                    predicates = predicates.And(x => x.Status == (int)status);
                }
                else
                {
                    return new ResponseGetFoodOrderHistoriesDto
                    {
                        StatusCode = 400,
                        Message = "Invalid order status"
                    };
                }
            }

            // Initialize the pageIndex and pageSize if not provided
            pageIndex ??= Constants.PageIndexDefault;
            pageSize ??= Constants.PageSizeDefault;

            // Include the order details
            includes = new[]
            {
                nameof(FoodOrderHistory.Items),
                nameof(FoodOrderHistory.Branch)
            };

            // Get the order history according to the branch of the receptionist
            var foodOrderHistories = await _unitOfWork.FoodOrderHistoryRepository!
                .GenericGetPaginationAsync(pageIndex.Value, pageSize.Value, predicates, includes);

            // Map the order history to the DTO
            var foodOrderHistoriesDto = _mapper.Map<List<FoodOrderHistoryDto>>(foodOrderHistories);

            // Calculate the total price for each order
            foreach (var order in foodOrderHistoriesDto)
            {
                order.TotalBill = order.Items?.Sum(item => item.TotalPrice) ?? 0;
            }

            // Get the total record
            var totalRecords = await _unitOfWork.FoodOrderHistoryRepository!
                .CountEntitiesAsync(predicates);

            // Calculate the total pages
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize.Value);

            return new ResponseGetFoodOrderHistoriesDto
            {
                StatusCode = 200,
                Message = "Get food order histories successfully",
                TotalRecord = totalRecords,
                TotalPage = totalPages,
                Data = foodOrderHistoriesDto,
            };
        }

        public async Task<ResponseGetFoodOrderHistoryDto> GetFoodOrderHistoryAsync(string userId, Guid id)
        {
            // Get the order by ID
            var order = await _unitOfWork.FoodOrderHistoryRepository!.GetByIdAsync(id, new[] { nameof(FoodOrderHistory.Items), nameof(FoodOrderHistory.Branch) });
            if (order == null)
            {
                return new ResponseGetFoodOrderHistoryDto
                {
                    StatusCode = 404,
                    Message = "Order not found"
                };
            }

            // Get the user by ID
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(Guid.Parse(userId),
                new[] {
                    nameof(User.BranchReceptionists),
                    nameof(User.Role)
                      });
            if (user == null)
            {
                return new ResponseGetFoodOrderHistoryDto
                {
                    StatusCode = 404,
                    Message = "User not found"
                };
            }

            // Check if the user is Admin and allowed to view the order
            if (user.Role.Name != Constants.RoleAdminName)
            {
                // Check if the user is allowed to view the order in the branch
                var branchId = user.BranchReceptionists
                    .Select(br => br.BranchId)
                    .FirstOrDefault();

                if (branchId != order.BranchId)
                {
                    return new ResponseGetFoodOrderHistoryDto
                    {
                        StatusCode = 403,
                        Message = "You are not allowed to view this order"
                    };
                }
            }

            // Map the order to the DTO
            var orderDto = _mapper.Map<FoodOrderHistoryDto>(order);

            return new ResponseGetFoodOrderHistoryDto
            {
                StatusCode = 200,
                Message = "Get food order history successfully",
                Data = orderDto
            };
        }

        public async Task<BaseResponseDto> UpdateOrderAsync(Guid id, RequestUpdateOrderDto request, Guid guid)
        {
            // Get the order by ID
            var order = await _unitOfWork.FoodOrderHistoryRepository!.GetByIdAsync(id);

            if (order == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "Order not found"
                };
            }

            // Include the BranchReceptionists to check if the user is allowed to update the order
            var includes = new[]
            {
                nameof(User.BranchReceptionists),
                nameof(User.Role)
            };

            // Get the user by ID
            var user = await _unitOfWork.UserRepository!.GetByIdAsync(guid, includes);

            if (user == null)
            {
                return new BaseResponseDto
                {
                    StatusCode = 404,
                    Message = "User not found"
                };
            }

            // Check if user is Admin and allowed to update the order
            if (user.Role.Name != Constants.RoleAdminName)
            {
                // Check if the user is allowed to update the order in the branch
                var branchId = user.BranchReceptionists
                    .Select(br => br.BranchId)
                    .FirstOrDefault();
                if (branchId != order.BranchId)
                {
                    return new BaseResponseDto
                    {
                        StatusCode = 403,
                        Message = "You are not allowed to update this order"
                    };
                }
            }

            // Update the status of the order
            if (Enum.TryParse<FoodOrderStatus>(request.Status, ignoreCase: true, out var status))
            {
                order.Status = (int)status;

                // Update the order
                _unitOfWork.FoodOrderHistoryRepository.Update(order);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseDto
                {
                    StatusCode = 200,
                    Message = "Order updated successfully"
                };
            }
            else
            {
                return new BaseResponseDto
                {
                    StatusCode = 400,
                    Message = "Invalid order status"
                };
            }
        }
    }
}
