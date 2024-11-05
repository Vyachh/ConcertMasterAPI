using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConcertMasterAPI.Models.EventModels
{
    /// <summary>
    /// Содержит информацию о событии.
    /// </summary>
    public class EventInformation
    {
        /// <summary>
        /// Жанры событий.
        /// </summary>
        public string[] EventGenres { get; set; }

        /// <summary>
        /// Типы событий.
        /// </summary>
        public string[] EventTypes { get; set; }

        /// <summary>
        /// Организаторы события.
        /// </summary>
        public IEnumerable<EventUserVM> Organizers { get; set; }

        /// <summary>
        /// Артисты, участвующие в событии.
        /// </summary>
        public IEnumerable<EventUserVM> Artists { get; set; }
    }

    /// <summary>
    /// Представление пользователя события, содержащее основные данные для отображения.
    /// </summary>
    public class EventUserVM
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// DTO-класс для данных события, включая основную информацию, жанр, тип и участников.
    /// </summary>
    public class EventInformationDto
    {
        /// <summary>
        /// Название события.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Жанр события.
        /// </summary>
        [JsonPropertyName("genre")]
        public string EventGenre { get; set; }

        /// <summary>
        /// Тип события.
        /// </summary>
        [JsonPropertyName("type")]
        public string EventType { get; set; }

        /// <summary>
        /// Дата и время начала события.
        /// </summary>
        [JsonPropertyName("eventStart")]
        public DateTime EventStart { get; set; }

        /// <summary>
        /// Название места проведения.
        /// </summary>
        [JsonPropertyName("addressVenue")]
        public string VenueName { get; set; }

        /// <summary>
        /// Список идентификаторов организаторов.
        /// </summary>
        [JsonPropertyName("organizers")]
        public IEnumerable<Guid> Organizers { get; set; }

        /// <summary>
        /// Список идентификаторов артистов.
        /// </summary>
        [JsonPropertyName("artists")]
        public IEnumerable<Guid> Artists { get; set; }
    }
}