using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using CryptoDiscordBot.Crypto;

namespace CryptoDiscordBot.Discord
{
    public class GetPriceModule : ModuleBase<SocketCommandContext>
    {
        [Command("getprice")]
        [Summary("Get the price of a ticker - !getprice exchange ticker")]
        public async Task GetPriceAsync(string exchange, string ticker)
        {
            IExchange _exchange = AlertManager.getExchange(exchange);
            string response;

            try
            {
                double price = await _exchange.getPriceAsync(ticker);
                response = String.Format("{0} {1} -  Current Price = {2}", exchange, ticker, price);
            }
            catch(TickerNotFoundException ex)
            {
                response = ex.Message;
            }

            await ReplyAsync(response);
        }
    }
}
