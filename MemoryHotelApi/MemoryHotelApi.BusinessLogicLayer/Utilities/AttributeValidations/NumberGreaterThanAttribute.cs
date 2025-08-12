using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MemoryHotelApi.BusinessLogicLayer.Utilities.AttributeValidations
{
    public class NumberGreaterThanAttribute : ValidationAttribute
    {
        private readonly object _minValue;

        public NumberGreaterThanAttribute(object minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value.GetType().IsAssignableTo(typeof(INumber<>)))
            {
                return new ValidationResult(ErrorMessage ?? "Vui lòng nhập số hợp lệ.");
            }
            if (value is double number && (number <= double.Parse(_minValue.ToString() ?? "0") || number > double.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is decimal decimalNumber && (decimalNumber <= decimal.Parse(_minValue.ToString() ?? "0") || decimalNumber > decimal.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is int intNumber && (intNumber <= int.Parse(_minValue.ToString() ?? "0") || intNumber > int.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is long longNumber && (longNumber <= long.Parse(_minValue.ToString() ?? "0") || longNumber > long.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is float floatNumber && (floatNumber <= float.Parse(_minValue.ToString() ?? "0") || floatNumber > float.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is short shortNumber && (shortNumber <= short.Parse(_minValue.ToString() ?? "0") || shortNumber > short.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is byte byteNumber && (byteNumber <= byte.Parse(_minValue.ToString() ?? "0") || byteNumber > byte.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is sbyte sbyteNumber && (sbyteNumber <= sbyte.Parse(_minValue.ToString() ?? "0") || sbyteNumber > sbyte.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is uint uintNumber && (uintNumber <= uint.Parse(_minValue.ToString() ?? "0") || uintNumber > uint.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is ulong ulongNumber && (ulongNumber <= ulong.Parse(_minValue.ToString() ?? "0") || ulongNumber > ulong.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }
            if (value is ushort ushortNumber && (ushortNumber <= ushort.Parse(_minValue.ToString() ?? "0") || ushortNumber > ushort.MaxValue))
            {
                return new ValidationResult(ErrorMessage ?? $"The field must be greater than {_minValue}.");
            }

            return ValidationResult.Success!;
        }
    }
}
