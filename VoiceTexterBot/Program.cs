using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using Telegram.Bots.Requests;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;

namespace VoiceTexterBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder().ConfigureServices((hostcontext,services) => ConfigureServices(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();

            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services) 
        {
            AppSettings appSettings = BuildAppSettings();

            services.AddSingleton(BuildAppSettings());

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<VoiceMessageController>();

            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings() 
        {         
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\evmor\\Downloads",
                BotToken = "6141760219:AAF_-GomYmE8cvT7gszjmDPT7RKGCwKxEHc",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
            };
        }
    }
}