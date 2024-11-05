// Ignore Spelling: Lcz Jwt

namespace ConcertMasterAPI.Helpers
{
    /// <summary>
    /// Содержит константы, используемые в приложении, включая сообщения и тексты ошибок.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Сообщение об ошибке, указывающее на то, что запись уже существует.
        /// </summary>
        public static string ObjectExists = "Такая запись уже существует";

        /// <summary>
        /// Содержит локализованные сообщения и тексты ошибок для операций, связанных с пользователями.
        /// </summary>
        public static class UserLcz
        {
            /// <summary>
            /// Сообщение об ошибке, указывающее на невозможность получения идентификатора пользователя из JWT.
            /// </summary>
            public static string UserIdJwtNotFound = "Не удалось получить идентификатор пользователя из JWT";

            /// <summary>
            /// Сообщение об ошибке, указывающее на неверный логин или пароль.
            /// </summary>
            public static string LoginOrPasswordIncorrectMessage = "Логин или пароль неверен";
        }

        /// <summary>
        /// Содержит локализованные сообщения и тексты ошибок для операций, связанных с билетами.
        /// </summary>
        public static class TicketLcz
        {
            /// <summary>
            /// Сообщение об ошибке, указывающее на отсутствие билета.
            /// </summary>
            public static string TicketNotFound = "Билета нет";

            /// <summary>
            /// Сообщение об ошибке, указывающее на недостаточное количество билетов для указанного типа.
            /// </summary>
            public static string TicketCountNotEnough = "Недостаточно билетов для типа {0}. Доступно: {1}";
        }
    }
}
