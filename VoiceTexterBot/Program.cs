using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;

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
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6141760219:AAF_-GomYmE8cvT7gszjmDPT7RKGCwKxEHc"));

            services.AddHostedService<Bot>();
        }
    }
}