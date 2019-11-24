using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot.Discord
{
    public class AddAlertModule : ModuleBase<SocketCommandContext>
    {

        [Command("add")]
        [Summary("Add an alert - !add exchange ticker price")]
        public async Task AddAlertAsync(string exchange, string ticker, double price, [Remainder] string comment="")
        {
            string response = await AlertManager.AddAlertCommand(exchange, ticker, price, comment);
            await ReplyAsync(response);
        }
    }
}
