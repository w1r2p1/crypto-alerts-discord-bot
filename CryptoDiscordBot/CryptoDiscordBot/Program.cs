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

namespace CryptoDiscordBot
{
    class Program
    {
        SocketTextChannel alertNotificationsChannel;

        // Command line arguements - discordBotToken serverId channelId
        static void Main(string[] args)
        {
            new Program().MainAsync(args).GetAwaiter().GetResult();
        }

        public async Task MainAsync(string[] args)
        {
            // Parse command line arguements
            string discordToken = args[0];
            ulong serverId = ulong.Parse(args[1]);
            ulong channelId = ulong.Parse(args[2]);

            // Initialize discord client
            DiscordSocketClient client = new DiscordSocketClient();
            client.Log += Log;

            CommandService cs = new CommandService();
            CommandHandler ch = new CommandHandler(client, cs);
            await ch.InstallCommandsAsync();

            // Login and start the bot
            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();

            // Wait some time to properly start and get the channel for the bot to message to 
            await Task.Delay(5000);
            alertNotificationsChannel = client.GetGuild(serverId).GetTextChannel(channelId);
            Console.WriteLine("Discord alerts channel set.");

            // Check all alerts constantly to see if they have been triggered
            AlertManager alertManager = new AlertManager();
            while(true)
            {
                var alerts = alertManager.getAllAlerts();
                foreach(var alert in alerts.ToArray())
                {
                    bool triggered = await alertManager.CheckAlertAsync(alert);
                    if(triggered)
                    {
                        string discordMessage = $"ALERT TRIGGERED - {alert.ToString()}. Current price: {((decimal)(alert.CurrentPrice)).ToString()}";
                        await alertNotificationsChannel.SendMessageAsync(discordMessage);
                        alertManager.removeAlert(alert);
                    }
                }
                await Task.Delay(10);
            }

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        

    }
}
