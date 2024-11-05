using System;

namespace ConcertMasterAPI.Models.TicketModels
{
    /// <summary>
    /// Модель, представляющая покупку билета пользователем.
    /// </summary>
    public class UserTicket
    {
        /// <summary>
        /// Уникальный идентификатор покупки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, купившего билет.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор приобретенного билета.
        /// </summary>
        public Guid TicketId { get; set; }

        /// <summary>
        /// Количество приобретенных билетов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Дата покупки билета.
        /// </summary>
        public DateTime PurchaseDate { get; set; }
    }
}
