// Ignore Spelling: Dto

using ConcertMasterAPI.Models.EventModels;
using ConcertMasterAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConcertMasterAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления информацией о событиях.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        /// <summary>
        /// Контекст базы данных для доступа к информации о событиях.
        /// </summary>
        private readonly EventService eventService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EventController"/>.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EventController(EventService eventService)
        {
            this.eventService = eventService;
        }

        /// <summary>
        /// Возвращает информацию о жанрах, типах событий, организаторах и артистах.
        /// </summary>
        /// <returns>Объект с информацией о жанрах, типах событий, организаторах и артистах.</returns>
        [HttpGet("GetEventInfo")]
        public EventInformation GetEventInformation()
        {
            return eventService.GetEventInformation();
        }

        /// <summary>
        /// Возвращает полный список событий с информацией о категориях, организаторах, артистах и билетах.
        /// </summary>
        /// <returns>Список событий с подробной информацией.</returns>
        [HttpGet("GetEvents")]
        public List<EventDto> GetEvents()
        {
            return eventService.GetEvents();
        }

        /// <summary>
        /// Добавляет новое событие в базу данных.
        /// </summary>
        /// <param name="eventDto">Данные события, которые нужно добавить.</param>
        /// <returns>Статус операции и данные добавленного события.</returns>
        [HttpPost("AddEvent")]
        public IActionResult AddEvent([FromBody] EventInformationDto eventDto)
        {
            return eventService.AddEvent(eventDto);
        }
    }
}