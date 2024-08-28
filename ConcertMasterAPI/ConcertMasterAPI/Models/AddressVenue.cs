using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Models
{
    public class AddressVenue
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string VenueName { get; set; }

        [Required]
        [MaxLength(200)]
        public string StreetAddress { get; set; } 

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string State { get; set; } 

        [Required]
        [MaxLength(50)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } 

        [MaxLength(100)]
        public string Coordinates { get; set; }

        public int Capacity { get; set; } 

        public ICollection<Event> Events { get; set; }
    }

}
