using ConcertMasterAPI.Context;
using ConcertMasterAPI.Helpers;
using ConcertMasterAPI.Helpers.Models;
using ConcertMasterAPI.Models.TicketModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ConcertMasterAPI.Services
{
    /// <summary>
    /// Сервис для работы с билетами.
    /// </summary>
    public class TicketService
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly PgContext context;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketService"/> с контекстом базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public TicketService(PgContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Выполняет покупку билетов пользователем.
        /// </summary>
        /// <param name="ticket">Информация о билете для покупки.</param>
        /// <param name="userId">Идентификатор пользователя, совершающего покупку.</param>
        /// <returns>Возвращает результат операции: Ok при успешной покупке, BadRequest при ошибке.</returns>
        public IActionResult BuyTickets(Ticket ticket, string userId)
        {
            if (ticket == null)
                return new BadRequestObjectResult(Constants.TicketLcz.TicketNotFound);

            if (!TicketHelper.ValidateTicket(ticket, out var validationResults))
                return new BadRequestObjectResult(validationResults);

            var existingTicket = context.Tickets
                .FirstOrDefault(t => t.EventId == ticket.EventId && t.Type == ticket.Type);

            if (existingTicket == null || existingTicket.Count < ticket.Count)
                return new BadRequestObjectResult(
                    string.Format(Constants.TicketLcz.TicketCountNotEnough, ticket.Type, existingTicket?.Count ?? 0));

            existingTicket.Count -= ticket.Count;
            context.UserTickets.Add(new UserTicket
            {
                UserId = Guid.Parse(userId),
                TicketId = existingTicket.Id,
                Count = ticket.Count,
                PurchaseDate = DateTime.UtcNow
            });
            context.Tickets.Update(existingTicket);

            var success = context.SaveChanges() > 0;

            return success ? new OkObjectResult(success) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Добавляет новые билеты для события.
        /// </summary>
        /// <param name="tickets">Массив добавляемых билетов.</param>
        /// <returns>Возвращает результат операции: Ok при успешном добавлении, BadRequest при ошибке.</returns>
        public IActionResult AddTickets(Ticket[] tickets)
        {
            if (tickets == null || tickets.Length == 0)
                return new BadRequestObjectResult(Constants.TicketLcz.TicketNotFound);

            if (!TicketHelper.ValidateTickets(tickets, out var validationResults))
                return new BadRequestObjectResult(validationResults);

            var existingTickets = context.Tickets
                .Where(t => t.EventId == tickets[0].EventId)
                .ToList();

            foreach (var ticket in tickets)
            {
                var existingTicket = existingTickets.FirstOrDefault(t => t.Type == ticket.Type);

                if (existingTicket != null)
                {
                    existingTicket.Count += ticket.Count;
                    existingTicket.Price = ticket.Price;
                }
                else
                {
                    context.Tickets.Add(ticket);
                }
            }

            var success = context.SaveChanges() > 0;
            return success ? new OkObjectResult(success) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
