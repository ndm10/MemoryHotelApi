namespace MemoryHotelApi.BusinessLogicLayer.Common
{
    public class Constants
    {
        public const int MaxFailedAttempts = 5;
        public const string RoleUserName = "User";
        public const string RoleAdminName = "Admin";
        public const string RoleReceptionistName = "Receptionist";
        public const int PageSizeDefault = 10;
        public const int PageIndexDefault = 1;
        public const int TokenExpiredTime = 1; // in day
    }
}
