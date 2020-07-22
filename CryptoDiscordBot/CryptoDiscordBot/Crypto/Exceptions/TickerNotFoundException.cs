using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoDiscordBot.Crypto
{
    class TickerNotFoundException : Exception
    {
        public TickerNotFoundException()
        {

        }

        public TickerNotFoundException(string message) : base(message)
        {

        }
    }
}
