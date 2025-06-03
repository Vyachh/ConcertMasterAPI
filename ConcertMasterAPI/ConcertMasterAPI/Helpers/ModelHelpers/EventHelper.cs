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
        public static Event MapEvent(EventInformationDto eventDto, IPgContext context)
        {
            ValidateEventInformation(eventDto);

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

        /// <summary>
        /// Проверяет обязательные поля объекта <see cref="EventInformationDto"/>.
        /// </summary>
        /// <param name="eventDto">Данные события для проверки.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если обязательные поля не заполнены.</exception>
        private static void ValidateEventInformation(EventInformationDto eventDto)
        {
            if (string.IsNullOrWhiteSpace(eventDto.Title))
                throw new ArgumentException("Поле Title обязательно для заполнения.", nameof(eventDto.Title));

            if (string.IsNullOrWhiteSpace(eventDto.Description))
                throw new ArgumentException("Поле Description обязательно для заполнения.", nameof(eventDto.Description));

            if (string.IsNullOrWhiteSpace(eventDto.EventGenre))
                throw new ArgumentException("Поле EventGenre обязательно для заполнения.", nameof(eventDto.EventGenre));

            if (string.IsNullOrWhiteSpace(eventDto.EventType))
                throw new ArgumentException("Поле EventType обязательно для заполнения.", nameof(eventDto.EventType));

            if (eventDto.EventStart == default)
                throw new ArgumentException("Поле EventStart обязательно для заполнения.", nameof(eventDto.EventStart));

            if (string.IsNullOrWhiteSpace(eventDto.VenueName))
                throw new ArgumentException("Поле VenueName обязательно для заполнения.", nameof(eventDto.VenueName));
        }
    }
}
