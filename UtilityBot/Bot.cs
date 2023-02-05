using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace UtilityBot
{
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramBotClient;

        public Bot(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             _telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, new ReceiverOptions() { AllowedUpdates = { } }, cancellationToken: stoppingToken);

            Console.WriteLine("Bot started");
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {            
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _telegramBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"Данный тип сообщений не поддерживается. Пожалуйста отправьте текст.", cancellationToken: cancellationToken);
                return;
            }
           
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _telegramBotClient.SendTextMessageAsync(update.Message.From.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: cancellationToken);
                        return;
                    default: 
                        await _telegramBotClient.SendTextMessageAsync(update.Message.From.Id, $"Данный тип сообщений не поддерживается. Пожалуйста отправьте текст.", cancellationToken: cancellationToken);
                        return;
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Waiting 10 seconds before retry");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}

