using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConcertMasterAPI.Models
{
    /// <summary>
    /// Класс, представляющий пользователя системы, с различными данными.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля пользователя.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Тип пользователя (покупатель, артист, организатор и т.д.).
        /// </summary>
        public UserType Type { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Ссылки на социальные сети пользователя.
        /// </summary>
        public ICollection<SocialMediaLink> SocialMediaLinks { get; set; }
    }

    /// <summary>
    /// Класс, представляющий ссылку на социальную сеть пользователя.
    /// </summary>
    public class SocialMediaLink
    {
        /// <summary>
        /// Уникальный идентификатор ссылки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Платформа социальной сети.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Platform { get; set; }

        /// <summary>
        /// URL-адрес профиля пользователя в социальной сети.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Url { get; set; }

        /// <summary>
        /// Идентификатор артиста, которому принадлежит эта ссылка.
        /// </summary>
        public Guid ArtistId { get; set; }
    }

    /// <summary>
    /// Класс представления пользователя для операций регистрации и входа.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Логин пользователя, обязательное поле.
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя, обязательное поле.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Электронная почта пользователя, необязательное поле.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона пользователя, необязательное поле.
        /// </summary>
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Перечисление, представляющее типы пользователей.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// Пользователь, совершающий покупки.
        /// </summary>
        Buyer,

        /// <summary>
        /// Артист, представляющий свое творчество.
        /// </summary>
        Artist,

        /// <summary>
        /// Организатор событий.
        /// </summary>
        Organizer,

        /// <summary>
        /// Администратор системы.
        /// </summary>
        Administator
    }
}
