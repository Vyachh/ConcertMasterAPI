using ConcertMasterAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ConcertMasterAPI.Context
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> options)
            : base(options)
        {
        }

        public DbSet<AddressVenue> Venues { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}