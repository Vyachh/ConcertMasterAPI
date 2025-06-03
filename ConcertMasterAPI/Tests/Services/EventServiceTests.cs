using ConcertMasterAPI.Models.EventModels;
using Microsoft.AspNetCore.Mvc;
using ConcertMasterAPI.Context;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ConcertMasterAPI.Models;
using ConcertMasterAPI.Services;

namespace ConcertMasterAPI.Test.Services
{
	/// <summary>
	/// Тестовый класс для проверки функциональности сервиса мероприятий.
	/// </summary>
	[TestClass()]
    public class EventServiceTests
    {
		/// <summary>
		/// Экземпляр сервиса мероприятий, тестируемый в данном классе.
		/// </summary>
		private EventService _service;

		/// <summary>
		/// Мок-объект контекста базы данных для взаимодействия с данными.
		/// </summary>
		private IPgContext _context;

		/// <summary>
		/// Мок-коллекция событий для имитации таблицы мероприятий в базе данных.
		/// </summary>
		private DbSet<Event> _mockEventsDbSet;

		/// <summary>
		/// Мок-коллекция пользователей для имитации таблицы пользователей в базе данных.
		/// </summary>
		private DbSet<User> _mockUsersDbSet;

		/// <summary>
		/// Инициализирует тестовый контекст, создавая мок-объекты базы данных и экземпляр тестируемого сервиса.
		/// </summary>
		[TestInitialize]
        public void Setup()
        {
			_context = Substitute.For<IPgContext>();
			_mockEventsDbSet = Substitute.For<DbSet<Event>, IQueryable<Event>>();
			_mockUsersDbSet = Substitute.For<DbSet<User>, IQueryable<User>>();
			_context.Events.Returns(_mockEventsDbSet);
			_context.Users.Returns(_mockUsersDbSet);
			_service = new EventService(_context);
		}

		/// <summary>
		/// Проверяет успешное добавление валидного мероприятия.
		/// </summary>
		/// <remarks>
		/// Тест проверяет корректное сохранение мероприятия в БД и возврат статуса 200 OK при валидных входных данных.
		/// </remarks>
		[TestMethod]
        public void AddEvent_ValidEvent_ReturnsOk()
        {
			// Arrange
			var eventDto = CreateTestEventDto();
			var artistId = eventDto.Artists.First();
			var organizerId = eventDto.Organizers.First();
			
			SetupUsers(
				new User { Id = artistId },
				new User { Id = organizerId }
			);

			SetupEvents(); // Пустой список событий

			_context.SaveChanges().Returns(1);

			// Act
			var result = _service.AddEvent(eventDto) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(eventDto, result.Value);

			_context.Events.Received(1).Add(Arg.Is<Event>(e =>
				e.Title == eventDto.Title &&
				e.Description == eventDto.Description &&
				e.Category.Genre == Enum.Parse<EventGenre>(eventDto.EventGenre) &&
				e.Category.Type == Enum.Parse<EventType>(eventDto.EventType) &&
				e.AddressVenue.VenueName == eventDto.VenueName &&
				e.Artists.Count == 1 &&
				e.Organizers.Count == 1
			));
			_context.Received(1).SaveChanges();
		}

		/// <summary>
		/// Проверяет обработку дубликата названия мероприятия.
		/// </summary>
		/// <remarks>
		/// Тест гарантирует возврат ошибки 400 Bad Request при попытке добавления мероприятия с уже существующим названием.
		/// </remarks>
		[TestMethod]
        public void AddEvent_DuplicateEventTitle_ReturnsBadRequest()
        {
			// Arrange
			var eventDto = CreateTestEventDto();

			SetupEvents(new Event { Title = "Concert" });
			SetupUsers(); // Пустой список пользователей

			// Act
			var result = _service.AddEvent(eventDto) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
			Assert.IsTrue(result.Value.ToString().Contains(Constants.ObjectExists));

			_context.Events.DidNotReceive().Add(Arg.Any<Event>());
			_context.DidNotReceive().SaveChanges();
		}

		/// <summary>
		/// Проверяет обработку невалидного DTO мероприятия.
		/// </summary>
		/// <remarks>
		/// Тест проверяет возврат ошибки 400 Bad Request и отсутствие изменений в БД при передаче null в качестве параметра.
		/// </remarks>
		[TestMethod]
        public void AddEvent_InvalidEventDto_ReturnsBadRequest()
        {
			// Arrange
			EventInformationDto eventDto = null;

			// Act
			var result = _service.AddEvent(eventDto) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);

			_context.DidNotReceive().Events.Add(Arg.Any<Event>());
			_context.DidNotReceive().SaveChanges();
		}

		/// <summary>
		/// Создает тестовый объект DTO мероприятия с настраиваемыми параметрами.
		/// </summary>
		/// <param name="title">Название мероприятия.</param>
		/// <param name="artistId">Идентификатор артиста (опционально).</param>
		/// <param name="organizerId">Идентификатор организатора (опционально).</param>
		/// <returns>Объект EventInformationDto с тестовыми данными.</returns>
		private EventInformationDto CreateTestEventDto(
			string title = "Concert",
			Guid? artistId = null,
			Guid? organizerId = null)
		{
			return new EventInformationDto
			{
				Title = title,
				Description = "Description",
				EventGenre = EventGenre.Indie.ToString(),
				EventStart = DateTime.Now,
				EventType = EventType.PrivateEvent.ToString(),
				Artists = [artistId ?? Guid.NewGuid()],
				Organizers = [organizerId ?? Guid.NewGuid()],
				VenueName = "Venue"
			};
		}

		/// <summary>
		/// Настраивает мок-коллекцию пользователей для тестирования.
		/// </summary>
		/// <param name="users">Массив пользователей для инициализации коллекции.</param>
		private void SetupUsers(params User[] users)
		{
			var usersData = users.AsQueryable();
			((IQueryable<User>)_mockUsersDbSet).Provider.Returns(usersData.Provider);
			((IQueryable<User>)_mockUsersDbSet).Expression.Returns(usersData.Expression);
			((IQueryable<User>)_mockUsersDbSet).GetEnumerator().Returns(usersData.GetEnumerator());
		}

		/// <summary>
		/// Настраивает мок-коллекцию мероприятий для тестирования.
		/// </summary>
		/// <param name="events">Массив мероприятий для инициализации коллекции.</param>
		private void SetupEvents(params Event[] events)
		{
			var eventsData = events.AsQueryable();
			((IQueryable<Event>)_mockEventsDbSet).Provider.Returns(eventsData.Provider);
			((IQueryable<Event>)_mockEventsDbSet).Expression.Returns(eventsData.Expression);
			((IQueryable<Event>)_mockEventsDbSet).GetEnumerator().Returns(eventsData.GetEnumerator());
		}
	}
}