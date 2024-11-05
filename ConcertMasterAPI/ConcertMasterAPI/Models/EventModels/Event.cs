// Ignore Spelling: Dto

using System;
using System.Collections.Generic;
using System.Linq;
using ConcertMasterAPI.Models.TicketModels;

namespace ConcertMasterAPI.Models.EventModels
{
    /// <summary>
    /// Событие.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Уникальный идентификатор события.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Коллекция билетов, связанных с событием.
        /// </summary>
        public IEnumerable<Ticket> Tickets { get; set; }

        /// <summary>
        /// Категория события.
        /// </summary>
        public EventCategory Category { get; set; }

        /// <summary>
        /// Название события.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание события.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата создания события.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата последнего обновления информации о событии.
        /// </summary>
        public DateTime Updated { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата и время начала события.
        /// </summary>
        public DateTime EventStart { get; set; }

        /// <summary>
        /// Адрес и место проведения события.
        /// </summary>
        public AddressVenue AddressVenue { get; set; }

        /// <summary>
        /// Общее количество билетов для события.
        /// </summary>
        public int TicketCount
        {
            get => Tickets?.Count() != null ? (int)Tickets?.Count() : 0;
        }

        /// <summary>
        /// Список организаторов события.
        /// </summary>
        public List<User> Organizers { get; set; }

        /// <summary>
        /// Список артистов, участвующих в событии.
        /// </summary>
        public List<User> Artists { get; set; }
    }

    /// <summary>
    /// Объект передачи данных для события, включающий дополнительную информацию о жанре и типе.
    /// </summary>
    public class EventDto : Event
    {
        /// <summary>
        /// Название жанра события.
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Название типа события.
        /// </summary>
        public string TypeName { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; } // Убедитесь, что это определение корректно

    }

    /// <summary>
    /// Класс, представляющий категорию события, содержащую информацию о жанре и типе.
    /// </summary>
    public class EventCategory
    {
        /// <summary>
        /// Уникальный идентификатор категории события.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Жанр события.
        /// </summary>
        public EventGenre Genre { get; set; }

        /// <summary>
        /// Тип события.
        /// </summary>
        public EventType Type { get; set; }
    }

    /// <summary>
    /// Перечисление, представляющее возможные жанры событий.
    /// </summary>
    public enum EventGenre
    {
        /// <summary>Рок.</summary>
        Rock,
        /// <summary>Поп.</summary>
        Pop,
        /// <summary>Джаз.</summary>
        Jazz,
        /// <summary>Классическая музыка.</summary>
        Classical,
        /// <summary>Хип-хоп.</summary>
        HipHop,
        /// <summary>Электронная музыка.</summary>
        Electronic,
        /// <summary>Кантри.</summary>
        Country,
        /// <summary>Блюз.</summary>
        Blues,
        /// <summary>Регги.</summary>
        Reggae,
        /// <summary>Фолк.</summary>
        Folk,
        /// <summary>Метал.</summary>
        Metal,
        /// <summary>Ритм-н-блюз.</summary>
        RnB,
        /// <summary>Инди.</summary>
        Indie,
        /// <summary>Панк.</summary>
        Punk,
        /// <summary>Латино.</summary>
        Latin,
        /// <summary>Соул.</summary>
        Soul,
        /// <summary>Диско.</summary>
        Disco,
        /// <summary>Фанк.</summary>
        Funk,
        /// <summary>Мировая музыка.</summary>
        WorldMusic,
        /// <summary>Альтернативная музыка.</summary>
        Alternative

    }

    /// <summary>
    /// Перечисление, представляющее возможные типы событий.
    /// </summary>
    public enum EventType
    {
        /// <summary>Концерт.</summary>
        Concert,
        /// <summary>Фестиваль.</summary>
        Festival,
        /// <summary>Конференция.</summary>
        Conference,
        /// <summary>Мастер-класс.</summary>
        Workshop,
        /// <summary>Встреча с артистами.</summary>
        MeetAndGreet,
        /// <summary>Вечеринка по случаю выхода альбома.</summary>
        AlbumReleaseParty,
        /// <summary>Благотворительное мероприятие.</summary>
        CharityEvent,
        /// <summary>Церемония награждения.</summary>
        AwardShow,
        /// <summary>Клубная ночь.</summary>
        ClubNight,
        /// <summary>Открытый микрофон.</summary>
        OpenMic,
        /// <summary>Частное мероприятие.</summary>
        PrivateEvent,
        /// <summary>Онлайн-трансляция.</summary>
        LiveStream,
        /// <summary>Турне.</summary>
        Tour,
        /// <summary>Сет диджея.</summary>
        DJSet,
        /// <summary>Прослушивание (альбома, треков и т.д.).</summary>
        ListeningSession
    }
}
