using ConcertMasterAPI.Models.TicketModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Helpers.Models
{
    /// <summary>
    /// Вспомогательный класс для валидации билетов.
    /// </summary>
    public class TicketHelper
    {
        /// <summary>
        /// Валидирует отдельный билет.
        /// </summary>
        /// <param name="ticket">Экземпляр билета, который требуется валидировать.</param>
        /// <param name="validationResults">Результаты валидации, если она не пройдена.</param>
        /// <returns>Возвращает true, если билет прошел валидацию, иначе false.</returns>
        public static bool ValidateTicket(Ticket ticket, out object validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(ticket, new ValidationContext(ticket), (ICollection<ValidationResult>)validationResults, true);
        }

        /// <summary>
        /// Валидирует массив билетов.
        /// </summary>
        /// <param name="tickets">Массив билетов, которые требуется валидировать.</param>
        /// <param name="validationResults">Список результатов валидации для каждого билета.</param>
        /// <returns>Возвращает true, если все билеты прошли валидацию, иначе false.</returns>
        public static bool ValidateTickets(Ticket[] tickets, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            foreach (var ticket in tickets)
            {
                if (!Validator.TryValidateObject(ticket, new ValidationContext(ticket), validationResults, true))
                    return false;
            }
            return true;
        }
    }
}
