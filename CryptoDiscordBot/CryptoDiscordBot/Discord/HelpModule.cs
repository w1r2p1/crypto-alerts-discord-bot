using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot.Discord
{
    public class HelpMoudle : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        [Summary("List all of the available commands")]
        public async Task AddAlertAsync()
        {
            string response = "List of available commands: \n";
            response += "!add exchange ticker price - Adds an alert for the specified parameters \n";
            response += "!remove id - Removes the alert specified by its ID \n";
            response += "!list - Lists all of the current untriggered alerts added \n";
            response += "!help - the command you just entered\n";
            await ReplyAsync(response);
        }
    }
}
