using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace VoiceTexterBot.Controllers
{
    internal class InlineKeyBoardController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyBoardController(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }
    }
}
