using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<SocialMediaLink> SocialMediaLinks { get; set; }
    }

    public class SocialMediaLink
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Platform { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; } 

        public Artist Artist { get; set; }
    }
}
