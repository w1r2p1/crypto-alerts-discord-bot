using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    static class AlertManager
    {

        private static Bittrex bittrex = new Bittrex();
        private static List<Alert> alerts = new List<Alert>();
        private static int alertId = 0;

        public static async Task<string> AddAlertCommand(string exchange, string ticker, double price)
        {
            // Determine direction of the alert
            IExchange _exchange = getExchange(exchange);
            double currentPrice = await _exchange.getPrice(ticker);

            Comparison comparison;
            if(currentPrice < price)
            {
                comparison = Comparison.Above;
            }
            else
            {
                comparison = Comparison.Below;
            }

            Alert alert = new Alert(ticker, exchange, price, comparison, alertId);
            alerts.Add(alert);
            alertId++;

            string botResponse = $"Alert added ID {alert.Id}: {ticker} {comparison} {price} on {exchange}";
            return botResponse;

        }

        public static async Task<string> ListAlertsCommand()
        {
            string botResponse = String.Empty;
            foreach(Alert alert in alerts)
            {
                string sAlert = $"ID {alert.Id}: {alert.Ticker} {alert.Comparison} {alert.Price} on {alert.Exchange} \n";
                botResponse = String.Concat(botResponse, sAlert);
            }

            return botResponse;
        }

        public static async Task<string> RemoveAlertCommand(int id)
        {
            Alert toDelete = new Alert(null, null, -1, Comparison.Above, id);
            alerts.Remove(toDelete);

            return "Deleted";
        }

        private static async Task<bool> CheckAlertAsync(Alert alert)
        {
            if (alert.Triggered) return true;

            IExchange exchange = getExchange(alert.Exchange);
            double currentPrice = await exchange.getPrice(alert.Ticker);

            if (alert.Comparison.Equals(Comparison.Above))
            {
                if (alert.Price > currentPrice)
                {
                    // alert set off
                    alert.Triggered = true;
                    return alert.Triggered;
                }
            }
            else
            {
                if (alert.Price < currentPrice)
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
