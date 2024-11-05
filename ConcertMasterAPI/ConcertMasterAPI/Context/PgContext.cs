using Bogus;
using ConcertMasterAPI.Models;
using ConcertMasterAPI.Models.EventModels;
using ConcertMasterAPI.Models.TicketModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcertMasterAPI.Context
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public class PgContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных с заданными параметрами.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста базы данных.</param>
        public PgContext(DbContextOptions<PgContext> options)
            : base(options)
        {

        }

        /// <summary>Таблица мест проведения мероприятий.</summary>
        public DbSet<AddressVenue> Venues { get; set; }

        /// <summary>Таблица событий.</summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>Таблица категорий событий.</summary>
        public DbSet<EventCategory> EventCategories { get; set; }

        /// <summary>Таблица заказов.</summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>Таблица билетов.</summary>
        public DbSet<Ticket> Tickets { get; set; }

        /// <summary>Таблица пользователей.</summary>
        public DbSet<User> Users { get; set; }

        /// <summary>Таблица билетов, купленных пользователями.</summary>
        public DbSet<UserTicket> UserTickets { get; set; }

        /// <summary>
        /// Метод для заполнения данных тестовыми пользователями.
        /// </summary>
        /// <param name="count">Количество пользователей для генерации.</param>
        public void SeedData(int count)
        {
            if (!Users.Any())
            {
                var users = GenerateFakeUsers(count);
                Users.AddRange(users);
                SaveChanges();
            }
        }

        /// <summary>
        /// Генерирует список тестовых пользователей.
        /// </summary>
        /// <param name="count">Количество пользователей для генерации.</param>
        /// <returns>Список тестовых пользователей.</returns>
        private static List<User> GenerateFakeUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Login, f => f.Internet.UserName())
                .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Type, f => f.PickRandom<UserType>())
                .RuleFor(u => u.Name, f => f.Random.Words(2));

            return faker.Generate(count);
        }
    }
}