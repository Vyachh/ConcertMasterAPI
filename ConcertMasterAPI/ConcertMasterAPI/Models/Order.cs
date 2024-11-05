using System;
using System.Collections.Generic;
using ConcertMasterAPI.Models.TicketModels;

namespace ConcertMasterAPI.Models
{
    /// <summary>
    /// Класс, представляющий заказ, содержащий информацию о покупателе и билетах.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Уникальный идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Покупатель, сделавший заказ.
        /// </summary>
        public User Buyer { get; set; }

        /// <summary>
        /// Дата создания заказа.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Список билетов, входящих в заказ.
        /// </summary>
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
