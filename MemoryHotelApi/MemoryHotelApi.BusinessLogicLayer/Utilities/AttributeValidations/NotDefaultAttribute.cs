using System.ComponentModel.DataAnnotations;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations
{
    public class NotDefaultAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is Guid guid && guid == Guid.Empty)
            {
                return new ValidationResult(ErrorMessage ?? "The field is not valid.");
            }
            return ValidationResult.Success!;
        }
    }
}
