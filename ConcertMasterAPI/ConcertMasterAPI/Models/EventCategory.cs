using System;

namespace ConcertMasterAPI.Models
{
    public class EventCategory
    {
        public Guid Id { get; set; }
        public EventGenre Genre { get; set; }
        public EventType Type { get; set; }
    }
}
