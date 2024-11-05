using ConcertMasterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ConcertMasterAPI.Helpers.Models
{
    /// <summary>
    /// Вспомогательный класс для работы с информацией о пользователе.
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// Извлекает идентификатор пользователя (Claim) из JWT токена в заголовке запроса.
        /// </summary>
        /// <param name="request">HTTP-запрос, содержащий заголовок с JWT токеном.</param>
        /// <returns>Возвращает Claim с идентификатором пользователя. Если идентификатор не найден, возвращает null.</returns>
        public static Claim GetUserIdFromToken(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");

            return userIdClaim;
        }

        /// <summary>
        /// Генерирует JWT-токен для аутентифицированного пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя, для которого создается токен.</param>
        /// <returns>Строка JWT-токена.</returns>
        public static string GenerateJwtToken(IConfiguration configuration, User user)
        {
            ObjectValidator.Validate(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey:DefaultSecretKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
