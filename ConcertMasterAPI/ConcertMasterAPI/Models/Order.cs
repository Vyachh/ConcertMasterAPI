using System;
using System.Collections;
using System.Collections.Generic;

namespace ConcertMasterAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User Buyer { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
