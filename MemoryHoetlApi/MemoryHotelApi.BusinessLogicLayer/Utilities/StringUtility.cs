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

            // Step 1: Remove emojis/icons
            string slug = Regex.Replace(input, @"\p{So}|\p{Cs}|\p{Cn}", "");

            // Step 2: Convert to lowercase
            slug = slug.ToLowerInvariant();

            // Step 3: Remove Vietnamese diacritics (assuming you have this method)
            slug = RemoveVietnameseDiacritics(slug);

            // Step 4: Remove special characters (keep letters, digits, spaces, hyphens)
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Step 5: Replace multiple spaces with a single hyphen
            slug = Regex.Replace(slug, @"\s+", "-");

            // Step 6: Replace multiple hyphens with a single hyphen
            slug = Regex.Replace(slug, @"-+", "-");

            // Step 7: Trim leading/trailing hyphens
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

        public string FormatHashtagName(string hashtag)
        {
            // Check if the hashtag is null or empty
            if (string.IsNullOrWhiteSpace(hashtag))
                return string.Empty;

            // Trim whitespace
            hashtag = hashtag.Trim();

            // Replace multiple spaces with a single space
            hashtag = Regex.Replace(hashtag, @"\s+", " ");

            // Upper only the first letter of hashtag
            if (hashtag.Length > 0)
            {
                hashtag = char.ToUpper(hashtag[0]) + hashtag.Substring(1).ToLower();
            }

            // Return the formatted hashtag
            return hashtag;
        }

        public string FomartStringNameCategory(string categoryName)
        {
            // Check if the category name is null or empty
            if (string.IsNullOrWhiteSpace(categoryName))
                return string.Empty;

            // Trim whitespace
            categoryName = categoryName.Trim();

            // Replace multiple spaces with a single space
            categoryName = Regex.Replace(categoryName, @"\s+", " ");

            //// Upper only the first letter of the name
            //if (categoryName.Length > 0)
            //{
            //    categoryName = char.ToUpper(categoryName[0]) + categoryName.Substring(1).ToLower();
            //}

            // Return the formatted category name
            return categoryName;
        }
    }
}
