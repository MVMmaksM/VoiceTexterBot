using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;

namespace UtilityBot
{
    internal class Program 
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder().ConfigureServices((hostcontext, services) => ConfigureServices(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();

            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<TextMessageController>();      

            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<IOperation, Operation>();

            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6156594854:AAGGUk0mvAyxoMOiq804ir0IRJ5CX9POk9U",
            };
        }
    }
}

