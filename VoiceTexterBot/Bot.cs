using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bots.Extensions.Polling;

namespace VoiceTexterBot
{
    internal class Bot
    {
        private ITelegramBotClient _telegramClient;

        public Bot(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            //нажатие на кнопки
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id,"Вы нажали на кнопку", cancellationToken:cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Отправилви сообщение", cancellationToken: cancellationToken);
                return;
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) 
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидание 10 секунд перед повторным подключением");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

    }
}
