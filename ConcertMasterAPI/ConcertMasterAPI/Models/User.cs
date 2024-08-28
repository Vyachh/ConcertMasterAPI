using System;
using System.Collections;
using System.Collections.Generic;

namespace ConcertMasterAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Buyer,
        Artist,
        Organizer,
        Administator
    }
}
