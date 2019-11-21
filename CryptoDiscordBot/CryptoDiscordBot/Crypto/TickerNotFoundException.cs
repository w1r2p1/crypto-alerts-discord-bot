using System;

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
