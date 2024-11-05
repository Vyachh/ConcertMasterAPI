// Ignore Spelling: Dto

using ConcertMasterAPI.Models.EventModels;
using ConcertMasterAPI.Models;
using System;
using ConcertMasterAPI.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConcertMasterAPI.Helpers.Models
{
    /// <summary>
    /// Вспомогательный класс для работы с <see cref="Event"/>.
    /// </summary>
    public class EventHelper
    {
        /// <summary>
        /// Преобразует объект <see cref="EventInformationDto"/> в объект <see cref="Event"/>.
        /// </summary>
        /// <param name="eventDto">Данные события для преобразования.</param>
        /// <returns>Новый объект <see cref="Event"/>.</returns>
        public static Event MapEvent(EventInformationDto eventDto, PgContext context)
        {
            var users = context.Users
                .Where(u => eventDto.Organizers.Contains(u.Id) || eventDto.Artists.Contains(u.Id))
                .ToList();

            return new Event()
            {
                Id = new Guid(),
                Title = eventDto.Title,
                Description = eventDto.Description,
                Category = new EventCategory()
                {
                    Id = new Guid(),
                    Genre = Enum.Parse<EventGenre>(eventDto.EventGenre),
                    Type = Enum.Parse<EventType>(eventDto.EventType)
                },
                EventStart = eventDto.EventStart,
                AddressVenue = new AddressVenue()
                {
                    Id = new Guid(),
                    VenueName = eventDto.VenueName
                },
                Organizers = users.Where(u => eventDto.Organizers.Contains(u.Id)).ToList(),
                Artists = users.Where(u => eventDto.Artists.Contains(u.Id)).ToList()
            };
        }
    }
}
