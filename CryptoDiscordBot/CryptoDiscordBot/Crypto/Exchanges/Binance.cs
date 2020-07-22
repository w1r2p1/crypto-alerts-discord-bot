using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace CryptoDiscordBot.Crypto
{
    class Binance : IExchange
    {
        public async Task<double> getPriceAsync(string ticker)
        {
            WebClient wc = new WebClient();
            string url = String.Format("https://api.binance.com/api/v3/avgPrice?symbol={0}", ticker);

            string response = await wc.DownloadStringTaskAsync(new Uri(url));
            dynamic json = JsonConvert.DeserializeObject(response);

            double price = json.price;
            return price;
        }
    }
}
