using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ConcertMasterAPI.Models;
using Microsoft.EntityFrameworkCore;
using ConcertMasterAPI.Context;
using NSubstitute;
using ConcertMasterAPI.Services;

namespace ConcertMasterAPI.Test.Services
{
    [TestClass()]
    public class UserServiceTests
    {
		private UserService _service;
		private IPgContext _context;
		private IConfiguration _configuration;
		private DbSet<User> _mockUsers;

		[TestInitialize]
        public void Setup()
        {
			// Настройка мока конфигурации
			var mockSection = Substitute.For<IConfigurationSection>();
			mockSection.Value.Returns("MySecretKey123rjo23pr32ht89234hgiopwjgf-ew9g0uew890g32pgj23gwgpiw3gjw398gpw3ujp");

			_configuration = Substitute.For<IConfiguration>();
			_configuration.GetSection("SecretKey:DefaultSecretKey").Returns(mockSection);

			// Настройка мока контекста
			_context = Substitute.For<IPgContext>();
			_mockUsers = Substitute.For<DbSet<User>, IQueryable<User>>();
			_context.Users.Returns(_mockUsers);

			_service = new UserService(_context, _configuration);
		}

        [TestMethod]
        public void Register_ValidUser_ReturnsOkResultWithToken()
        {
			// Arrange
			var userDto = new UserDto
			{
				Login = "newuser",
				Password = "password123",
				PhoneNumber = "1234567890",
				Email = "newuser@example.com",
			};

			// Настраиваем пустой список пользователей
			SetupDbSet(_mockUsers, new List<User>());
			_context.SaveChanges().Returns(1);

			// Act
			var result = _service.Register(userDto) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.IsTrue(result.Value.ToString().Contains("token"));

			// Проверяем что пользователь был добавлен
			_mockUsers.Received(1).Add(Arg.Any<User>());
			_context.Received(1).SaveChanges();
		}

        [TestMethod]
        public void Register_ExistingUser_ReturnsBadRequest()
        {
			// Arrange
			var existingUser = new User { Login = "existinguser" };
			SetupDbSet(_mockUsers, new List<User> { existingUser });

			var userDto = new UserDto
			{
				Login = "existinguser",
				Password = "password123",
				PhoneNumber = "1234567890",
				Email = "existinguser@example.com"
			};

			// Act
			var result = _service.Register(userDto) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
			Assert.IsTrue(result.Value.ToString().Contains(Constants.ObjectExists));

			// Проверяем что Add не вызывался
			_mockUsers.DidNotReceive().Add(Arg.Any<User>());
			_context.DidNotReceive().SaveChanges();
		}

        [TestMethod]
        public void Login_ValidUser_ReturnsOkResultWithToken()
        {
			// Arrange
			var user = new User
			{
				Login = "validuser",
				PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123")
			};

			SetupDbSet(_mockUsers, new List<User> { user });

			var userDto = new UserDto
			{
				Login = "validuser",
				Password = "password123"
			};

			// Act
			var result = _service.Login(userDto) as OkObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
			Assert.IsTrue(result.Value.ToString().Contains("token"));
		}

        [TestMethod]
        public void Login_InvalidUser_ReturnsBadRequest()
        {
			// Arrange
			SetupDbSet(_mockUsers, new List<User>());

			var userDto = new UserDto
			{
				Login = "invaliduser",
				Password = "wrongpassword"
			};

			// Act
			var result = _service.Login(userDto) as BadRequestObjectResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
			Assert.IsTrue(result.Value.ToString().Contains(Constants.UserLcz.LoginOrPasswordIncorrectMessage));
		}

		private static void SetupDbSet<T>(DbSet<T> mockSet, IEnumerable<T> data) where T : class
		{
			var queryable = data.AsQueryable();
			((IQueryable<T>)mockSet).Provider.Returns(queryable.Provider);
			((IQueryable<T>)mockSet).Expression.Returns(queryable.Expression);
			((IQueryable<T>)mockSet).GetEnumerator().Returns(queryable.GetEnumerator());
		}
	}
}