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
using System.Collections.Generic;

namespace CryptoDiscordBot
{
    class Program
    {
        SocketTextChannel alertNotificationsChannel;

        static void Main(string[] args)
        {
            string[] _args = new string[1];
            if(args.Length == 0)
            {
                Console.WriteLine("Please enter the path to the config file:");
                _args[0] = Console.ReadLine().Trim();
            }
            else
            {
                args.CopyTo(_args, 0);
            }

            new Program().MainAsync(_args).GetAwaiter().GetResult();
        }

        public async Task MainAsync(string[] args)
        {
            // Parse command line arguements
            Config config = Config.readConfigFile(args[0]);

            string discordToken = config.DiscordBotToken;
            ulong serverId = ulong.Parse(config.DiscordServerId);
            ulong channelId = ulong.Parse(config.DiscordChannelId);

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

            AlertManager alertManager;
            if (config.DatabaseCredentialsSet)
            {
                alertManager = new AlertManager(config.DatabaseServerUrl, config.DatabasePort, config.DatabaseName, config.DatabaseTable, config.DatabaseUser, config.DatabasePassword);
                Console.WriteLine("Database connection made and all stored alerts grabbed.");
            }
            else
            {
                alertManager = new AlertManager();
                Console.WriteLine("No database connection, alerts will be stored in memory only.");
            }

            // Check all alerts constantly to see if they have been triggered
            while (true)
            {
                var alerts = alertManager.getAllAlerts();
                var checkedAlerts = new List<Alert>();

                foreach(var alert in alerts.ToArray())
                {
                    if (checkedAlerts.Contains(alert)) continue;

                    var sameMarketAlerts = alertManager.getSameMarketAlerts(alert);
                    double price = await alertManager.getAlertPriceAsync(alert);

                    foreach(var _alert in sameMarketAlerts)
                    {
                        bool triggered = await alertManager.CheckAlertAsync(alert, price);
                        if (triggered)
                        {
                            string discordMessage = $"ALERT TRIGGERED - {alert.ToString()}. Current price: {((decimal)(alert.CurrentPrice)).ToString()}";
                            await alertNotificationsChannel.SendMessageAsync(discordMessage);
                            alertManager.removeAlert(alert);
                        }
                        checkedAlerts.Add(_alert);
                    }
                   
                }
                await Task.Delay(2000);
            }
            
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        

    }
}
