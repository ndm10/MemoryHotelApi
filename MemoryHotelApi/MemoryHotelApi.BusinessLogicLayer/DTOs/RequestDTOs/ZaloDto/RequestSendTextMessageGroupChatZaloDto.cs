using System.Text.Json.Serialization;

namespace MemoryHotelApi.BusinessLogicLayer.DTOs.RequestDTOs.ZaloDto
{
    public class RequestSendTextMessageGroupChatZaloDto
    {
        [JsonPropertyName("recipient")]
        public Recipient Recipient { get; set; } = null!;

        [JsonPropertyName("message")]
        public Message Message { get; set; } = null!;
    }

    public class Recipient
    {
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; } = null!;
    }

    public class Message
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = null!;
    }
}
