/// <summary>
/// Константные сообщения, используемые в приложении, которые могут потребоваться в разных слоях.
/// </summary>
public static class Constants
{
	/// <summary>
	/// Сообщение об ошибке, указывающее на то, что объект уже существует.
	/// </summary>
	public static string ObjectExists = "Объект уже существует";

	/// <summary>
	/// Локализованные сообщения об ошибках для модуля, связанного с пользователями.
	/// </summary>
	public static class UserLcz
	{
		/// <summary>
		/// Сообщение об ошибке, указывающее на отсутствие идентификатора пользователя в JWT.
		/// </summary>
		public static string UserIdJwtNotFound = "Не удалось получить идентификатор пользователя из JWT";

		/// <summary>
		/// Сообщение об ошибке, указывающее на неверный логин или пароль.
		/// </summary>
		public static string LoginOrPasswordIncorrectMessage = "Логин или пароль неверный";
	}

	/// <summary>
	/// Локализованные сообщения об ошибках для модуля, связанного с билетами.
	/// </summary>
	public static class TicketLcz
	{
		/// <summary>
		/// Сообщение об ошибке, указывающее на отсутствие билета.
		/// </summary>
		public static string TicketNotFound = "Билет не найден";

		/// <summary>
		/// Сообщение об ошибке, указывающее на недостаточное количество билетов для покупки.
		/// </summary>
		public static string TicketCountNotEnough = "Недостаточно билетов типа {0}. Доступно: {1}";
	}
}