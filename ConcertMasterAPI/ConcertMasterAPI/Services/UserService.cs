using ConcertMasterAPI.Context;
using ConcertMasterAPI.Helpers;
using ConcertMasterAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using ConcertMasterAPI.Helpers.Models;

namespace ConcertMasterAPI.Services
{
    /// <summary>
    /// Сервис для работы с сущностью <see cref="User"/>
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Контекст базы данных для взаимодействия с таблицами пользователей.
        /// </summary>
        private readonly PgContext context;

        /// <summary>
        /// Интерфейс конфигурации для доступа к настройкам приложения.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserService"/> с заданным контекстом базы данных и конфигурацией.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public UserService(PgContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="userVM">Данные пользователя для регистрации.</param>
        /// <returns>Результат регистрации с токеном или сообщением об ошибке.</returns>
        public IActionResult Register(UserDto userVM)
        {
            ObjectValidator.Validate(userVM);
            var existingUser = context.Users.FirstOrDefault(u => u.Login == userVM.Login);

            if (existingUser != null)
            {
                return new BadRequestObjectResult(new { statusText = Constants.ObjectExists });
            }

            var user = new User
            {
                Login = userVM.Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userVM.Password),
                PhoneNumber = userVM.PhoneNumber,
                Email = userVM.Email,
                Type = UserType.Buyer
            };

            context.Users.Add(user);

            if (context.SaveChanges() > 0)
            {
                var token = UserHelper.GenerateJwtToken(configuration, user);
                return new OkObjectResult(new { token });
            }

            return new BadRequestObjectResult(new { statusText = Constants.UserLcz.LoginOrPasswordIncorrectMessage });
        }

        /// <summary>
        /// Аутентификация пользователя.
        /// </summary>
        /// <param name="userDto">Данные пользователя для входа.</param>
        /// <returns>Токен при успешной аутентификации или сообщение об ошибке.</returns>
        public IActionResult Login(UserDto userDto)
        {
            ObjectValidator.Validate(userDto);

            var user = context.Users.FirstOrDefault(u => u.Login == userDto.Login);

            if (IsValidUser(userDto, user))
            {
                var token = UserHelper.GenerateJwtToken(configuration, user);
                return new OkObjectResult(new { token });
            }

            return new BadRequestObjectResult(new { statusText = Constants.UserLcz.LoginOrPasswordIncorrectMessage });
        }

        /// <summary>
        /// Проверяет, является ли пользователь допустимым, на основе переданных данных.
        /// </summary>
        /// <param name="userDto">Входные данные пользователя.</param>
        /// <param name="user">Объект пользователя из базы данных.</param>
        /// <returns>True, если пользователь допустим; в противном случае - False.</returns>
        private bool IsValidUser(UserDto userDto, User user)
        {
            if (userDto == null || user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash);
        }
    }
}