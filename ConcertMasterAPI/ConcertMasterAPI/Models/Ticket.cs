using System;

namespace ConcertMasterAPI.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }
        public int Price { get; set; }
        public User Buyer { get; set; }
    }

    public enum TicketStatus
    {
        InStock,
        Sold
    }

    public enum TicketType
    {
        Default,
        Vip,
        Student
    }
}
