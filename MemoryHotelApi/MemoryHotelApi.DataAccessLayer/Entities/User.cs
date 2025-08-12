namespace MemoryHotelApi.DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Nationality { get; set; }
        public bool IsVerified { get; set; }
        public Guid RoleId { get; set; }
        public Guid? MembershipTierId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool IsDeletedAllowed { get; set; }
        public Role Role { get; set; } = new Role();
        public MembershipTier? MembershipTier { get; set; }
        public ICollection<BranchReceptionist> BranchReceptionists { get; set; } = new List<BranchReceptionist>();
    }
}