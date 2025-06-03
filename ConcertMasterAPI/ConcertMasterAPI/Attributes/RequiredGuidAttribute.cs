using System.ComponentModel.DataAnnotations;
using System;

namespace ConcertMasterAPI.Attributes
{
    /// <summary>
    /// Атрибут обязательности Guid.
    /// </summary>
    public class RequiredGuidAttribute : ValidationAttribute
    {
		/// <summary>
		/// Возвращает результат валидации.
		/// </summary>
		/// <param name="value">Валидируемый объект.</param>
		/// <param name="validationContext">Контекст валидации.</param>
		/// <returns>Результат валидации.</returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Guid guid && guid == Guid.Empty)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.MemberName} cannot be Guid.Empty");
            }

            return ValidationResult.Success;
        }
    }
}
