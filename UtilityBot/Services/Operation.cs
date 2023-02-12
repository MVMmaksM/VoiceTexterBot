using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Services
{
    internal class Operation : IOperation
    {
        long IOperation.CountChar(string value)
        {
            if (value == null)
                return 0;

            return value.Length;
        }

        long IOperation.Sum(string value)
        {
            if (value == null)
                return 0;

            long result = default;

            foreach (var val in value.Split(' '))
            {
                result += long.Parse(val);
            }

            return result;
        }
    }
}
