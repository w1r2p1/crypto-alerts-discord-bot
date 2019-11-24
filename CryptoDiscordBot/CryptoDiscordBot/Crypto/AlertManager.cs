using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    class AlertManager
    {

        private static Bittrex bittrex = new Bittrex();
        private static Bitfinex bitfinex = new Bitfinex();
        private static List<Alert> alerts = new List<Alert>();
        private static int alertId = 0;

        public static async Task<string> AddAlertCommand(string exchange, string ticker, double price, string comment)
        {
            // Determine direction of the alert
            IExchange _exchange = getExchange(exchange);
            double currentPrice = -1;

            try
            {
                currentPrice = await _exchange.getPriceAsync(ticker);
            }catch(TickerNotFoundException tnfe)
            {
                return "Ticker/market not found, alert not added!";
            }

            Comparison comparison;
            if(currentPrice < price)
            {
                comparison = Comparison.Above;
            }
            else
            {
                comparison = Comparison.Below;
            }

            Alert alert = new Alert(ticker, exchange, price, comparison, alertId, comment);
            alerts.Add(alert);
            alertId++;

            string botResponse = $"Alert added {alert.ToString()}";
            return botResponse;

        }

        public static async Task<string> ListAlertsCommand()
        {
            if(alerts.Count == 0)
            {
                return "No alerts set!";
            }

            string botResponse = String.Empty;
            foreach(Alert alert in alerts)
            {
                string sAlert = $"{alert.ToString()} \n";
                botResponse = String.Concat(botResponse, sAlert);
            }

            return botResponse;
        }

        public static async Task<string> RemoveAlertCommand(int id)
        {
            Alert toDelete = new Alert(null, null, -1, Comparison.Above, id);
            alerts.Remove(toDelete);

            return "Alert removed";
        }

        public List<Alert> getAllAlerts()
        {
            return alerts;
        }

        public void removeAlert(Alert alert)
        {
            alerts.Remove(alert);
        }

        public async Task<bool> CheckAlertAsync(Alert alert)
        {
            if (alert.Triggered) return true;

            IExchange exchange = getExchange(alert.Exchange);
            double currentPrice = await exchange.getPriceAsync(alert.Ticker);

            if (alert.Comparison.Equals(Comparison.Above))
            {
                if (alert.Price < currentPrice)
                {
                    alert.Triggered = true;
                    alert.CurrentPrice = currentPrice;
                    return true;
                }
            }
            else
            {
                if (alert.Price > currentPrice)
                {
                    alert.Triggered = true;
                    alert.CurrentPrice = currentPrice;
                    return true;
                }
            }

            return false;

        }

        private static IExchange getExchange(string exchange)
        {
            if (exchange.Equals("bittrex", StringComparison.OrdinalIgnoreCase))
                return bittrex;
            if (exchange.Equals("bitfinex", StringComparison.OrdinalIgnoreCase))
                return bitfinex;
            return null;
        }

    }
}
