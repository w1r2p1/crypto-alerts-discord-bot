using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    interface IExchange
    {
        Task<double> getPrice(string ticker);
    }
}
