using ConcertMasterAPI.Models;
using ConcertMasterAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcertMasterAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Сервис для работы с пользователями.
        /// </summary>
        private readonly UserService userService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UserController"/> с указанным сервисом пользователей.
        /// </summary>
        /// <param name="userService">Экземпляр сервиса пользователей для выполнения операций с данными пользователей.</param>
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="userVM">Объект пользователя, содержащий данные для регистрации.</param>
        /// <returns>Результат операции регистрации с токеном или сообщением об ошибке.</returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDto userVM)
        {
            return userService.Register(userVM);
        }

        /// <summary>
        /// Выполняет вход пользователя в систему.
        /// </summary>
        /// <param name="userVM">Объект пользователя, содержащий данные для входа.</param>
        /// <returns>JWT-токен при успешном входе или сообщение об ошибке.</returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDto userVM)
        {
            return userService.Login(userVM);
        }
    }
}
