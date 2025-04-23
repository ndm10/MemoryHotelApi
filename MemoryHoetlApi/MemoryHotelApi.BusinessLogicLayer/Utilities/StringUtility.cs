using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities
{
    public class StringUtility
    {
        public string GenerateOtp()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            int otp = BitConverter.ToInt32(randomBytes, 0) & 0x7FFFFFFF;
            return (otp % 1000000).ToString("D6");
        }

        public string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string slug = input.ToLowerInvariant();

            slug = RemoveVietnameseDiacritics(slug);

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");

            slug = slug.Trim('-');

            return slug;
        }

        private static string RemoveVietnameseDiacritics(string text)
        {
            var vietnameseDiacriticMap = new Dictionary<string, string>
            {
                { "[àáảãạăắằẳẵặâấầẩẫậ]", "a" },
                { "[èéẻẽẹêếềểễệ]", "e" },
                { "[ìíỉĩị]", "i" },
                { "[òóỏõọôốồổỗộơớờởỡợ]", "o" },
                { "[ùúủũụưứừửữự]", "u" },
                { "[ỳýỷỹỵ]", "y" },
                { "[đ]", "d" }
            };

            string result = text;
            foreach (var map in vietnameseDiacriticMap)
            {
                result = Regex.Replace(result, map.Key, map.Value);
            }

            return result;
        }
    }
}
