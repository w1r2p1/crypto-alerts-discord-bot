using System;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {

            Bittrex x = new Bittrex();
            x.getTicker("BTC-LdTC").Wait();
            
        }
    }
}
