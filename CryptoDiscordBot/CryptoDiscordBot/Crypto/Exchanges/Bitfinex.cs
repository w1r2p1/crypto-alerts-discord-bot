using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDiscordBot.Crypto
{
    class Bitfinex : IExchange
    {
        private string apiEndpoint = "https://api-pub.bitfinex.com/v2/";

        public async Task<double> getPriceAsync(string ticker)
        {
            var price = await getTicker(ticker);
            return price;
        }

        private async Task<double> getTicker(string ticker)
        {
            string _ticker = ticker.ToUpper();
            _ticker = _ticker.Replace("-", "");

            string urlExtension = String.Format("ticker/t{0}", _ticker);
            string url = String.Concat(apiEndpoint, urlExtension);

            try
            {
                string response = await GetAsync(url); // [Bid,bid_size,ask,ask_size,daily_change,daily_change_perc,last_price,volume,high,low]

                string lastPrice = response.Split(',')[6];
                return double.Parse(lastPrice);
            }
            catch(Exception ex)
            {
                string msg = String.Format("Ticker {0} not found. Make sure the ticker is a valid {1} ticker, e.g. \"{2}\"", ticker, "Bitfinex", "ETHBTC");
                throw new TickerNotFoundException(msg);
            }

        }

        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
