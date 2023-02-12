using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Models;
using static System.Collections.Specialized.BitVector32;

namespace UtilityBot.Services
{
    internal class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        bool IStorage.ExistSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        Session IStorage.GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                return _sessions[chatId];
            }

            var newSession = new Session() { OperationCode = "sum" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
