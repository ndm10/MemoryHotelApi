using System.Security.Cryptography;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities
{
    public class StringGenerator
    {
        public string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            int otp = BitConverter.ToInt32(randomBytes, 0) & 0x7FFFFFFF;
            return (otp % 1000000).ToString("D6");
        }
    }
}
