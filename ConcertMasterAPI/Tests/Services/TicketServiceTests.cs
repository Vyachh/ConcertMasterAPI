using ConcertMasterAPI.Models.TicketModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ConcertMasterAPI.Context;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using ConcertMasterAPI.Test;
using ConcertMasterAPI.Services;

namespace ConcertMasterAPI.Test.Services
{
	/// <summary>
	/// Тестовый класс для проверки функциональности сервиса билетов.
	/// </summary>
	[TestClass()]
    public class TicketServiceTests
    {
		/// <summary>
		/// Мок-объект контекста базы данных.
		/// </summary>
		private IPgContext _context;

		/// <summary>
		/// Экземпляр тестируемого сервиса билетов.
		/// </summary>
		private TicketService _service;

		/// <summary>
		/// Мок-коллекция билетов для имитации таблицы в базе данных.
		/// </summary>
		private DbSet<Ticket> _mockTickets;

		/// <summary>
		/// Мок-коллекция пользовательских билетов для имитации таблицы в базе данных.
		/// </summary>
		private DbSet<UserTicket> _mockUserTickets;

		/// <summary>
		/// Инициализирует тестовое окружение, создавая мок-объекты и экземпляр сервиса.
		/// </summary>
		[TestInitialize]
        public void Setup()
        {
			_context = Substitute.For<IPgContext>();
			_mockTickets = Substitute.For<DbSet<Ticket>, IQueryable<Ticket>>();
			_mockUserTickets = Substitute.For<DbSet<UserTicket>, IQueryable<UserTicket>>();

			_context.Tickets.Returns(_mockTickets);
			_context.UserTickets.Returns(_mockUserTickets);

			_service = new TicketService(_context);
		}

		/// <summary>
		/// Проверяет успешную покупку билетов при валидном запросе.
		/// </summary>
		[TestMethod]
		public void BuyTickets_ValidRequest_ReturnsOk()
		{
			// Arrange
			var eventId = Guid.NewGuid();
			var ticket = new Ticket { Id = Guid.NewGuid(), EventId = eventId, Type = TicketType.Default, Count = 10, Price = 100 };

			SetupDbSet(_mockTickets, new List<Ticket> { ticket });
			SetupDbSet(_mockUserTickets, new List<UserTicket>());

			// Глушение вызовов (Stubbing) - Подмена возвращаемых значений методов

			_context.SaveChanges().Returns(1);

			var purchaseRequest = new Ticket { Id = Guid.NewGuid(), EventId = eventId, Type = TicketType.Default, Count = 2 };

			// Act
			var result = _service.BuyTickets(purchaseRequest, Guid.NewGuid().ToString()) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(true, result.Value);

			// Ограничения заглушек (Stub Constraints) - позволяют задавать условия для срабатывания заглушек

			_mockTickets.Received(1).Update(Arg.Is<Ticket>(t => t.Count == 8));
			_mockUserTickets.Received(1).Add(Arg.Any<UserTicket>());
			_context.Received(1).SaveChanges();
		}

		/// <summary>
		/// Проверяет обработку ситуации с недостатком доступных билетов.
		/// </summary>
		[TestMethod]
        public void BuyTickets_NotEnoughTickets_ReturnsBadRequest()
        {
			// Arrange
			var eventId = Guid.NewGuid();
			var ticket = new Ticket { EventId = eventId, Type = TicketType.Vip, Count = 1, Price = 100 };

			SetupDbSet(_mockTickets, new List<Ticket> { ticket });

			var purchaseRequest = new Ticket { EventId = eventId, Type = TicketType.Vip, Count = 2 };

			// Act
			var result = _service.BuyTickets(purchaseRequest, Guid.NewGuid().ToString()) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);

			var errorMessage = result.Value as string;
			Assert.IsNotNull(errorMessage);
			Assert.IsTrue(errorMessage.Contains(TestConstants.TicketTestServiceLcz.NotEnoughTickets));

			_mockTickets.DidNotReceive().Update(Arg.Any<Ticket>());
			_mockUserTickets.DidNotReceive().Add(Arg.Any<UserTicket>());
			_context.DidNotReceive().SaveChanges();
		}

		/// <summary>
		/// Проверяет успешное добавление новых билетов в систему.
		/// </summary>
		[TestMethod]
        public void AddTickets_ValidTickets_ReturnsOk()
        {
			// Arrange
			var tickets = new[]
			{
				new Ticket { EventId = Guid.NewGuid(), Type = TicketType.Vip, Count = 5, Price = 100 },
				new Ticket { EventId = Guid.NewGuid(), Type = TicketType.Default, Count = 10, Price = 50 }
			};

			SetupDbSet(_mockTickets, new List<Ticket>());
			_context.SaveChanges().Returns(1);

			// Act
			var result = _service.AddTickets(tickets) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(true, result.Value);

			_mockTickets.Received(1).AddRange(Arg.Is<IEnumerable<Ticket>>(t => t.Count() == 2));
			_context.Received(1).SaveChanges();
		}

		/// <summary>
		/// Проверяет обновление количества и цены существующих билетов.
		/// </summary>
		[TestMethod]
        public void AddTickets_TicketsAlreadyExist_UpdatesCountAndPrice()
        {
			// Arrange
			var eventId = Guid.NewGuid();
			var existingTicket = new Ticket
			{
				Id = Guid.NewGuid(),
				EventId = eventId,
				Type = TicketType.Vip,
				Count = 5,
				Price = 100
			};

			var ticketsData = new List<Ticket> { existingTicket }.AsQueryable();
			SetupDbSet(_mockTickets, ticketsData);

			_context.Tickets.Returns(_mockTickets);
			_context.SaveChanges().Returns(1);

			var newTickets = new[]
			{
				new Ticket {
					EventId = eventId,
					Type = TicketType.Vip,
					Count = 5,
					Price = 120
				},
			};

			// Act
			var result = _service.AddTickets(newTickets) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.AreEqual(true, result.Value);

			_mockTickets.Received(1).Update(Arg.Is<Ticket>(t =>
				t.Id == existingTicket.Id &&
				t.Count == 10 &&            
				t.Price == 120));

			// Регистрация вызовов (Call Tracking) - Проверка, был ли вызван метод

			_context.Received(1).SaveChanges();
		}

		/// <summary>
		/// Проверяет обработку пустого списка билетов при добавлении.
		/// </summary>
		[TestMethod]
        public void AddTickets_EmptyTickets_ReturnsBadRequest()
        {
			// Arrange
			var tickets = Array.Empty<Ticket>();

			// Act
			var result = _service.AddTickets(tickets) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);

			_mockTickets.DidNotReceive().AddRange(Arg.Any<IEnumerable<Ticket>>());
			_context.DidNotReceive().SaveChanges();
		}

		/// <summary>
		/// Создает мок-коллекцию для тестирования работы с базой данных.
		/// </summary>
		/// <typeparam name="T">Тип сущности базы данных.</typeparam>
		/// <param name="data">Тестовые данные для инициализации коллекции.</param>
		/// <returns>Настроенный мок-объект DbSet.</returns>
		public static Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.ToList().Add);
            mockSet.Setup(d => d.AddRange(It.IsAny<IEnumerable<T>>()))
                   .Callback<IEnumerable<T>>(items => data.ToList().AddRange(items));

            return mockSet;
        }

		/// <summary>
		/// Настраивает мок-коллекцию для работы с тестовыми данными.
		/// </summary>
		/// <typeparam name="T">Тип сущности базы данных.</typeparam>
		/// <param name="mockSet">Мок-объект коллекции.</param>
		/// <param name="data">Тестовые данные для настройки.</param>
		private static void SetupDbSet<T>(DbSet<T> mockSet, IEnumerable<T> data) where T : class
		{
			var queryable = data.AsQueryable();
			((IQueryable<T>)mockSet).Provider.Returns(queryable.Provider);
			((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
			((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());
		}
	}
}