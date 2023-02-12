using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;
        private readonly IOperation _operation;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IOperation operation)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _operation = operation;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");         

            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Посчитать количество символов в тексте" , $"countChar"),
                        InlineKeyboardButton.WithCallbackData($" Найти сумму чисел" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот подсчитывает количество символов и сумму чисел.</b> {Environment.NewLine}"
                        , cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;                 
                default:
                    if (_memoryStorage.ExistSession(message.Chat.Id))
                    {
                        var operationCode = _memoryStorage.GetSession(message.Chat.Id).OperationCode;

                        if (operationCode == "countChar")
                        {
                            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Количество символов: {_operation.CountChar(message.Text)}", cancellationToken: ct);
                        }
                        else if (operationCode == "sum")
                        {
                            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {_operation.Sum(message.Text)}", cancellationToken: ct); 
                        }                       
                    }
                    else
                    {
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Для выбора команды введите /start", cancellationToken: ct);
                    }                   
                    break;
            }
        }
    }
}
