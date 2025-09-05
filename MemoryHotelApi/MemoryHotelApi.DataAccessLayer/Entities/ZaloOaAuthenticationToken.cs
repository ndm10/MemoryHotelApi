namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class ZaloOaAuthenticationToken : BaseEntity
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }
        public bool IsUsed { get; set; }
    }
}
