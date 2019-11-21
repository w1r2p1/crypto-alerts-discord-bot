using System;
using CryptoDiscordBot.Crypto;
using System.Threading.Tasks;
using System.Threading;

namespace CryptoDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            Bittrex x = new Bittrex();
            double y = await x.getPrice("BTC-LTc");
            Console.WriteLine(y);
        }
    }
}
