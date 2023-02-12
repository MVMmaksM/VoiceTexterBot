using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityBot.Models;
using static System.Collections.Specialized.BitVector32;

namespace UtilityBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
        bool ExistSession(long chatId);
    }
}
