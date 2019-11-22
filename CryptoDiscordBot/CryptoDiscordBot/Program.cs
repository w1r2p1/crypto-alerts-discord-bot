using System;
using CryptoDiscordBot.Crypto;
using System.Threading.Tasks;
using System.Threading;
using Discord;
using Discord.API;
using Discord.Net;
using Discord.Rest;
using Discord.Webhook;
using Discord.Commands;
using Discord.WebSocket;

namespace CryptoDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().MainAsync(args).GetAwaiter().GetResult();
        }

        public async Task MainAsync(string[] args)
        {
            
        }

        
    }
}
