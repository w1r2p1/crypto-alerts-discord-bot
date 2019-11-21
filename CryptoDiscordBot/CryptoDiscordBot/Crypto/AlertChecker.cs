using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    static class AlertChecker
    {
        private static Bittrex bittrex = new Bittrex();

        public static async Task<bool> CheckAlertAsync(Alert alert)
        {
            if (alert.Triggered) return true;

            IExchange exchange = getExchange(alert.Exchange);
            double currentPrice = await exchange.getPrice(alert.Ticker);

            if(alert.Comparison.Equals(Comparison.Above))
            {
                if(alert.Price > currentPrice)
                {
                    // alert set off
                    alert.Triggered = true;
                    return alert.Triggered;
                }
            }
            else
            {
                if(alert.Price < currentPrice)
                {
                    // alert set off
                    alert.Triggered = true;
                    return alert.Triggered;
                }
            }

            return false;
            
        }

        private static IExchange getExchange(string exchange)
        {
            if (exchange.Equals("bittrex", StringComparison.OrdinalIgnoreCase))
                return bittrex;
            return null;
        }

    }
}
