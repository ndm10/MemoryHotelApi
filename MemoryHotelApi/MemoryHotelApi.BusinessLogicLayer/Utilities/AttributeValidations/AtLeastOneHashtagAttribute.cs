using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations
{
    public class AtLeastOneHashtagAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Vui lòng nhập ít nhất một hashtag.");
            }

            var hashtags = (List<string>)value;
            if (hashtags.Count == 0)
            {
                return new ValidationResult("Vui lòng nhập ít nhất một hashtag.");
            }

            return ValidationResult.Success;
        }
    }
}
