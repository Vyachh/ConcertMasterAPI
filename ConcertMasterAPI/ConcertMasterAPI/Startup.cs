using ConcertMasterAPI.Context;
using ConcertMasterAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ConcertMasterAPI
{
    /// <summary>
    /// Класс, отвечающий за конфигурацию приложения.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Startup"/> с указанной конфигурацией.
        /// </summary>
        /// <param name="configuration">Объект конфигурации приложения.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Получает объект конфигурации приложения.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Метод для добавления сервисов в контейнер зависимостей.
        /// </summary>
        /// <param name="services">Коллекция сервисов для добавления.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://127.0.0.1:5500",
                        ValidAudience = "http://127.0.0.1:5500",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecretKey:DefaultSecretKey")))
                    };
                });

            services.AddControllers();
            services.AddScoped<EventService>();
            services.AddScoped<TicketService>();
            services.AddScoped<UserService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConcertMasterAPI", Version = "v1" });
            });
            services.AddDbContext<PgContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();

                    builder.WithOrigins("http://127.0.0.1:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Метод для настройки приложения на основе предоставленного <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">Объект приложения.</param>
        /// <param name="env">Среда выполнения приложения.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConcertMasterAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
