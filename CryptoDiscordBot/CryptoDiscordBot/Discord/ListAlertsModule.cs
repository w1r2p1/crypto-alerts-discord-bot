using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot.Discord
{
    public class ListAlertsModule : ModuleBase<SocketCommandContext>
    {

        [Command("list")]
        [Summary("List all current alerts")]
        public async Task ListAlertsAsync()
        {
            string response = await AlertManager.ListAlertsCommand();
            await ReplyAsync(response);
        }
    }
}
