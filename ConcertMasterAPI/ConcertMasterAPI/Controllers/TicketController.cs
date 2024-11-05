using ConcertMasterAPI.Helpers;
using ConcertMasterAPI.Helpers.Models;
using ConcertMasterAPI.Models.TicketModels;
using ConcertMasterAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcertMasterAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления покупкой и добавлением билетов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        /// <summary>
        /// Сервис работы с билетами.
        /// </summary>
        private readonly TicketService _ticketService;

        /// <summary>
        /// Контроллер для билетов.
        /// </summary>
        /// <param name="ticketService">Экземпляр сервиса работы с билетами.</param>
        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Покупка билетов пользователем.
        /// </summary>
        /// <param name="ticket">Информация о билете для покупки.</param>
        /// <returns>Результат операции покупки.</returns>
        [HttpPost("BuyTickets")]
        public IActionResult BuyTickets([FromBody] Ticket ticket)
        {
            if (ticket == null)
                return BadRequest(Constants.TicketLcz.TicketNotFound);

            var userId = UserHelper.GetUserIdFromToken(Request);
            if (userId == null)
                return Unauthorized(Constants.UserLcz.UserIdJwtNotFound);

            return _ticketService.BuyTickets(ticket, userId.Value);
        }

        /// <summary>
        /// Добавляет новые билеты для события.
        /// </summary>
        /// <param name="tickets">Массив добавляемых билетов.</param>
        /// <returns>Результат операции добавления.</returns>
        [HttpPost("AddTickets")]
        public IActionResult AddTickets([FromBody] Ticket[] tickets)
        {
            return _ticketService.AddTickets(tickets);
        }
    }
}