using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace CryptoDiscordBot.Crypto
{
    class Bitmex : IExchange
    {
        public async Task<double> getPriceAsync(string ticker)
        {
            WebClient wc = new WebClient();
            string url = String.Format("https://www.bitmex.com/api/v1/trade?symbol={0}&count=1&reverse=true", ticker.ToUpper());

            try
            {
                string response = await wc.DownloadStringTaskAsync(new Uri(url));
                dynamic json = JsonConvert.DeserializeObject(response);

                double price = json[0].price;
                return price;
            }
            catch (Exception e)
            {
                string msg = String.Format("Ticker {0} not found. Make sure the ticker is a valid {1} ticker, e.g. \"{2}\"", ticker, "Bitmex", "XBTUSD");
                throw new TickerNotFoundException(msg);
            }
        }
    }
}
