using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Models.TicketModels
{
    /// <summary>
    /// Модель, представляющая билет на событие.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Уникальный идентификатор билета.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип билета (например, VIP, стандартный).
        /// </summary>
        public TicketType Type { get; set; }

        /// <summary>
        /// Статус билета (например, в наличии или продан).
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Цена билета.
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Количество доступных билетов.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Идентификатор события, к которому относится билет.
        /// </summary>
        public Guid EventId { get; set; }
    }

    /// <summary>
    /// Перечисление, представляющее статус билета.
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// Билет в наличии.
        /// </summary>
        InStock,

        /// <summary>
        /// Билет продан.
        /// </summary>
        Sold
    }

    /// <summary>
    /// Перечисление, представляющее тип билета.
    /// </summary>
    public enum TicketType
    {
        /// <summary>
        /// Обычный билет.
        /// </summary>
        Default,

        /// <summary>
        /// VIP билет.
        /// </summary>
        Vip,

        /// <summary>
        /// Билет для студентов.
        /// </summary>
        Student
    }
}
