using ConcertMasterAPI.Models.EventModels;
using ConcertMasterAPI.Models.TicketModels;
using ConcertMasterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConcertMasterAPI.Context
{
    public interface IPgContext
    {
        /// <summary>Таблица мест проведения мероприятий.</summary>
        DbSet<AddressVenue> Venues { get; set; }

        /// <summary>Таблица событий.</summary>
        DbSet<Event> Events { get; set; }

        /// <summary>Таблица категорий событий.</summary>
        DbSet<EventCategory> EventCategories { get; set; }

        /// <summary>Таблица заказов.</summary>
        DbSet<Order> Orders { get; set; }
        
        /// <summary>Таблица билетов.</summary>
        DbSet<Ticket> Tickets { get; set; }

        /// <summary>Таблица пользователей.</summary>
        DbSet<User> Users { get; set; }

        /// <summary>Таблица билетов, купленных пользователями.</summary>
        DbSet<UserTicket> UserTickets { get; set; }

        /// <summary>
        /// Сохранение изменений в базе данных.
        /// </summary>
        int SaveChanges();
    }
}