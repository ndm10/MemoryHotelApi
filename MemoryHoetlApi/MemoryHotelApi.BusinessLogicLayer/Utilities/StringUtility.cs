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

        public string UpperCaseFirstEachWord(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.Trim();
            input = Regex.Replace(input, @"\s+", " ");

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }

        public string UpperFirstLetter(string input)
        {
            // Check if the input is null or empty
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
                return string.Empty;
            
            input = input.Trim();
            input = Regex.Replace(input, @"\s+", " ");
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        public string GenerateRandomString()
        {
            // Generate a random string with special characters for password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+[]{}|;:,.<>?";
            const int length = 10; // Length of the random string
            var random = new Random();
            var randomString = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomString);
        }
    }
}
