using ConcertMasterAPI.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConcertMasterAPI
{
    /// <summary>
    /// Класс, представляющий точку входа приложения.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Основной метод, который запускает приложение.
        /// </summary>
        /// <param name="args">Массив аргументов командной строки.</param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length > 0 && args[0] == "seeddata")
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PgContext>();
                context.SeedData(20);
            }

            host.Run();
        }

        /// <summary>
        /// Создает и настраивает хост приложения.
        /// </summary>
        /// <param name="args">Массив аргументов командной строки.</param>
        /// <returns>Настроенный хост приложения.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
