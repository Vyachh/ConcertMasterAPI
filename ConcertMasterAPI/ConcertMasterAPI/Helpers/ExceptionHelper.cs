using System.Text;

namespace ConcertMasterAPI.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с исключениями.
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// Сообщение исключения, когда значение null.
        /// </summary>
        public static string NullExceptionMessage
        {
            get => "Значение не может быть null.";
        }
    }
}
