using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.ResponseDTOs.ZaloDto
{
    public class ResponseSendTextMessageGroupChatZaloDto
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; } = null!;
        [JsonPropertyName("error")]
        public int Error { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; } = string.Empty;
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = string.Empty;
    }
}
