using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace VoiceTexterBot.Controllers
{
    internal class DefaultMessageControlle
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public DefaultMessageControlle(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }
    }
}
