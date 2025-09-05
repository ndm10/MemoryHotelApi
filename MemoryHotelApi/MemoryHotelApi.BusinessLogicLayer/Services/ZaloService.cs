using MemoryHotelApi.BusinessLogicLayer.Common;
using MemoryHotelApi.BusinessLogicLayer.Common.Enums;
using MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ZaloDto;
using MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ZaloDto;
using MemoryHotelApi.BusinessLogicLayer.Services.Interface;
using MemoryHotelApi.DataAccessLayer.Entities;
using MemoryHotelApi.DataAccessLayer.UnitOfWork.Interface;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MemoryHotelApi.BusinessLogicLayer.Services
{
    public class ZaloService : IZaloService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public ZaloService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://openapi.zalo.me/");
        }

        public async Task<ResponseSendTextMessageGroupChatZaloDto> SendMessageToZaloGroupChatAsync(FoodOrderHistory foodOrderHistory)
        {
            // Get information of Zalo group chat
            var foodGroupChat = await _unitOfWork.GroupChatZaloRepository!.GetEntityWithConditionAsync(x => x.GroupType == Constants.GroupChatFoodOrder);
            if (foodGroupChat == null)
            {
                return new ResponseSendTextMessageGroupChatZaloDto
                {
                    Error = 1,
                    Message = "Zalo group chat not found"
                };
            }
            var groupId = foodGroupChat.GroupId;

            // Get access token from database
            var authenticationToken = await _unitOfWork.ZaloOaAuthenticationTokenRepository!.GetEntityWithConditionAsync(x => !x.IsUsed);
            if (authenticationToken == null)
            {
                return new ResponseSendTextMessageGroupChatZaloDto
                {
                    Error = 1,
                    Message = "Zalo authentication token not found"
                };
            }
            var accessToken = authenticationToken.AccessToken;

            // Map status to text for message
            var statusText = foodOrderHistory.Status switch
            {
                (int)FoodOrderStatus.Pending => "🟡 Đang đặt hàng",
                (int)FoodOrderStatus.Finished => "✅ Đã hoàn thành",
                (int)FoodOrderStatus.Cancelled => "❌ Đã bị hủy",
                _ => "Không xác định"
            };

            // Use Vietnamese culture for formatting
            var culture = CultureInfo.GetCultureInfo("vi-VN");

            // Format order items table
            var orderItems = string.Join("\n", foodOrderHistory.Items.Select(item =>
            $"{item.Name} x {item.Quantity} = {(item.Quantity * item.Price).ToString("N0", culture)}đ"));

            // Populate the template
            var message = $@"[{foodOrderHistory.OrderCode} || {foodOrderHistory.Branch.Name}]" + "\n" +
                            $"------------------------------------------------------------------------------------------" + "\n" +
                            $"Tên khách hàng: {foodOrderHistory.CustomerName}" + "\n" +
                            $"Số phòng: {foodOrderHistory.Room}" + "\n" +
                            $"Số điện thoại: {foodOrderHistory.CustomerPhone ?? "Không có thông tin"}" + "\n" +
                            $"Địa chỉ giao hàng: {foodOrderHistory.Branch.Address}" + "\n" +
                            $"Ngày đặt hàng: {DateTime.Now:yyyy-MM-dd HH:mm:ss}" + "\n" +
                            $"------------------------------------------------------------------------------------------" + "\n" +
                            $"Chi tiết đơn:" + "\n" +
                            $"{orderItems}" + "\n" +
                            "------------------------------------------------------------------------------------------" + "\n" +
                            $"Tổng tiền đơn đặt hàng: {foodOrderHistory.Items.Sum(x => x.TotalPrice).ToString("N0", culture)}đ" + "\n" +
                            $"Trạng thái: {statusText}" + "\n" +
                            $"Note: {foodOrderHistory.Note}";

            var requestDto = new RequestSendTextMessageGroupChatZaloDto
            {
                Recipient = new Recipient { GroupId = groupId },
                Message = new Message { Text = message }
            };

            // Set headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("access_token", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the payload to JSON
            var json = JsonSerializer.Serialize(requestDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request
            var response = await _httpClient.PostAsync("v3.0/oa/group/message", content);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Deserialize the response
            var responseJson = await response.Content.ReadAsStringAsync();
            var zaloResponse = JsonSerializer.Deserialize<ResponseSendTextMessageGroupChatZaloDto>(responseJson);

            return zaloResponse ?? new ResponseSendTextMessageGroupChatZaloDto
            {
                Error = 1,
                Message = "Failed to deserialize Zalo response"
            };
        }
    }
}
