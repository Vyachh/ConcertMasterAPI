using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConcertMasterAPI.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public EventCategory Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<User> Organizers { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; }
        public DateTime EventStart { get; set; }
        public AddressVenue AddressVenue { get; set; }
        public int TicketCount
        {
            get => Tickets.Count();
        }
        public ICollection<Artist> Artists { get; set; }
    }

    public enum EventGenre
    {

    }

    public enum EventType
    {

    }
}
