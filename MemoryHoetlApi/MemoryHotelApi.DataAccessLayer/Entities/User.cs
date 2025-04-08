namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class User : GenericEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Nationality { get; set; }
        public bool IsVerified { get; set; }
        public required Guid RoleId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Role? Role { get; set; }
    }
}
