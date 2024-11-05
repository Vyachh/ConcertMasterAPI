using ConcertMasterAPI.Models.EventModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Models
{
    /// <summary>
    /// Адрес проведения
    /// </summary>
    public class AddressVenue
    {
        /// <summary>
        /// Идентификатор адреса
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Название адреса
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string VenueName { get; set; }

        /// <summary>
        /// Улица адреса
        /// </summary>
        [MaxLength(200)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Штат
        /// </summary>
        [MaxLength(100)]
        public string State { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        [MaxLength(50)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [MaxLength(100)]
        public string Country { get; set; }

        /// <summary>
        /// Координаты
        /// </summary>
        [MaxLength(100)]
        public string Coordinates { get; set; }

        /// <summary>
        /// Численность
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Мероприятия
        /// </summary>
        public ICollection<Event> Events { get; set; }
    }
}