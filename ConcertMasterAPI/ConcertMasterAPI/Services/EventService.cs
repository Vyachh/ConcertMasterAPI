// Ignore Spelling: Dto

using ConcertMasterAPI.Context;
using ConcertMasterAPI.Helpers.Models;
using ConcertMasterAPI.Models.EventModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using ConcertMasterAPI.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConcertMasterAPI.Models;

namespace ConcertMasterAPI.Services
{
    /// <summary>
    /// Сервис для работы с событиями.
    /// </summary>
    public class EventService
    {
        /// <summary>
        /// Контекст базы данных.е
        /// </summary>
        private readonly PgContext context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EventService"/> с контекстом базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EventService(PgContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Получает информацию о событиях, включая жанры, типы, организаторов и артистов.
        /// </summary>
        /// <returns>Объект <see cref="EventInformation"/> с данными о событиях.</returns>
        public EventInformation GetEventInformation()
        {
            return new EventInformation
            {
                EventGenres = Enum.GetNames(typeof(EventGenre)),
                EventTypes = Enum.GetNames(typeof(EventType)),
                Organizers = context.Users
                    .Where(u => u.Type == UserType.Organizer)
                    .Select(u => new EventUserVM { UserId = u.Id, UserName = u.Name })
                    .ToList(),
                Artists = context.Users
                    .Where(u => u.Type == UserType.Artist)
                    .Select(u => new EventUserVM { UserId = u.Id, UserName = u.Name })
                    .ToList()
            };
        }

        /// <summary>
        /// Возвращает список событий с полной информацией.
        /// </summary>
        /// <returns>Список объектов <see cref="EventDto"/> с подробными данными о событиях.</returns>
        public List<EventDto> GetEvents()
        {
            var events = context.Events
           .Include(e => e.Category)
           .Include(e => e.Artists)
           .Include(e => e.Organizers)
           .Include(e => e.Tickets)
           .ToList();

            return events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                EventStart = e.EventStart,
                GenreName = e.Category.Genre.ToString(),
                TypeName = e.Category.Type.ToString(),
                Artists = e.Artists,
                Organizers = e.Organizers,
                Tickets = e.Tickets
            }).ToList();
        }

        /// <summary>
        /// Добавляет новое событие в базу данных.
        /// </summary>
        /// <param name="eventDto">Данные нового события.</param>
        /// <returns>Результат добавления события в базу данных.</returns>
        public IActionResult AddEvent(EventInformationDto eventDto)
        {
            if (context.Events.Any(e => e.Title == eventDto.Title))
            {
                return new BadRequestObjectResult(new { message = Constants.ObjectExists });
            }

            var newEvent = EventHelper.MapEvent(eventDto, context);
            context.Events.Add(newEvent);
            context.SaveChanges();

            return new OkObjectResult(eventDto);
        }
    }
}
