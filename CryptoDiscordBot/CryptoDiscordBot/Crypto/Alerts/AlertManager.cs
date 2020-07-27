using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    class AlertManager
    {
        private static Bittrex bittrex = new Bittrex();
        private static Bitfinex bitfinex = new Bitfinex();
        private static Bitmex bitmex = new Bitmex();
        private static Binance binance = new Binance();
        private static Kucoin kucoin = new Kucoin();

        private static List<Alert> alerts = new List<Alert>();
        private static int alertId = 0;

        private static Database.MySql database;

        public AlertManager()
        {
            database = null;
        }

        public AlertManager(string dbServer, int dbPort, string dbName, string dbTable, string dbUser, string dbPassword)
        {
            database = new Database.MySql(dbServer, dbPort, dbName, dbTable, dbUser, dbPassword);
            database.Connect();
            alerts = database.GetAllAlerts();
            alertId = alerts.Max(a => a.Id) + 1;
        }

        public List<Alert> getAllAlerts()
        {
            return alerts;
        }

        public List<Alert> getSameMarketAlerts(Alert alert)
        {
            List<Alert> sameMarketAlerts = new List<Alert>();
            foreach (var _alert in alerts.ToArray())
            {
                if (_alert.Exchange.Equals(alert.Exchange, StringComparison.OrdinalIgnoreCase) && _alert.Ticker.Equals(alert.Ticker, StringComparison.OrdinalIgnoreCase))
                    sameMarketAlerts.Add(_alert);
            }

            return sameMarketAlerts;
        }

        public void removeAlert(Alert alert)
        {
            alerts.Remove(alert);

            if(database != null)
                database.DeleteAlert(alert);
        }

        public async Task<double> getAlertPriceAsync(Alert alert)
        {
            IExchange exchange = getExchange(alert.Exchange);
            double currentPrice = await exchange.getPriceAsync(alert.Ticker);

            alert.CurrentPrice = currentPrice;
            return currentPrice;
        }

        public async Task<bool> CheckAlertAsync(Alert alert, double price = -1)
        {
            if (alert.Triggered) return true;

            double currentPrice;

            if (price == -1)
                currentPrice = await getAlertPriceAsync(alert);
            else
                currentPrice = price;

            if (alert.Comparison.Equals(Comparison.Above))
            {
                if (alert.Price < currentPrice)
                {
                    alert.Triggered = true;
                }
            }
            else
            {
                if (alert.Price > currentPrice)
                {
                    alert.Triggered = true;
                }
            }

            alert.CurrentPrice = currentPrice;
            return alert.Triggered;

        }

        public static async Task<string> AddAlertCommand(string exchange, string ticker, double price, string comment)
        {
            // Determine direction of the alert
            IExchange _exchange = getExchange(exchange);
            double currentPrice = -1;

            if (_exchange == null)
                return "Invalid exchange, alert not added";

            try
            {
                currentPrice = await _exchange.getPriceAsync(ticker);
            }catch(TickerNotFoundException exc)
            {
                return exc.Message;
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

            if(database != null)
                database.InsertAlert(alert);

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

            if(database != null)
                database.DeleteAlert(toDelete);

            return "Alert removed";
        }

        public static IExchange getExchange(string exchange)
        {
            if (exchange.Equals("bittrex", StringComparison.OrdinalIgnoreCase))
                return bittrex;
            if (exchange.Equals("bitfinex", StringComparison.OrdinalIgnoreCase))
                return bitfinex;
            if (exchange.Equals("bitmex", StringComparison.OrdinalIgnoreCase))
                return bitmex;
            if (exchange.Equals("binance", StringComparison.OrdinalIgnoreCase))
                return binance;
            if (exchange.Equals("kucoin", StringComparison.OrdinalIgnoreCase))
                return kucoin;

            return null;
        }

    }
}
