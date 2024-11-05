using System;

namespace ConcertMasterAPI.Models.TicketModels
{
    /// <summary>
    /// Модель запроса на покупку билета, содержащая информацию о пользователе и билете.
    /// </summary>
    public class BuyTicketRequest
    {
        /// <summary>
        /// Токен пользователя для аутентификации.
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// Информация о приобретаемом билете.
        /// </summary>
        public Ticket Ticket { get; set; }
    }
}
