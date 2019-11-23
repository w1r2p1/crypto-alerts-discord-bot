using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot.Discord
{
    public class RemoveAlertModule : ModuleBase<SocketCommandContext>
    {

        [Command("remove")]
        [Summary("Remove an alert - !remove alertID")]
        public async Task RemoveAlertAsync(int id)
        {
            string response = await AlertManager.RemoveAlertCommand(id);
            await ReplyAsync(response);
        }
    }
}
