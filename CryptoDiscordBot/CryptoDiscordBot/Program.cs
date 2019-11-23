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
using CryptoDiscordBot.Discord;
using Microsoft.Extensions.DependencyInjection;

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
            string discordToken = args[0]; 
            DiscordSocketClient client = new DiscordSocketClient();
            client.Log += Log;

            CommandService cs = new CommandService();
            CommandHandler ch = new CommandHandler(client, cs);

            await ch.InstallCommandsAsync();

            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}
