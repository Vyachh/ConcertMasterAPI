using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace ConcertMasterAPI.Helpers
{
    /// <summary>
    /// Валидатор объектов.
    /// </summary>
    public class ObjectValidator
    {
        /// <summary>
        /// Проверяет объект на null и выбрасывает исключение, если объект равен null.
        /// </summary>
        /// <param name="value">Объект для проверки.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если объект равен null.</exception>
        public static void Validate(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(ExceptionHelper.NullExceptionMessage);
            }
        }
    }
}
