namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class GroupChatZalo : BaseEntity
    {
        public string GroupId { get; set; } = null!;
        public int GroupType { get; set; }
    }
}
