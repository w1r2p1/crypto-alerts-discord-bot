using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CryptoDiscordBot.Crypto
{
    class Bittrex : IExchange
    {
        string apiEndpoint = "https://api.bittrex.com/api/v1.1/";

        public async Task<double> getPriceAsync(string ticker)
        {
            var price = await getTicker(ticker);
            return price;
        }

        private async Task<double> getTicker(string ticker)
        {
            string urlExtension = String.Format("public/getticker?market={0}", ticker);
            string url = String.Concat(apiEndpoint, urlExtension);

            try
            {
                string response = await GetAsync(url);

                var _response = JsonConvert.DeserializeObject<GetTicker.RootObject>(response);
                if (_response.message.Contains("invalid", StringComparison.OrdinalIgnoreCase))
                    throw new Exception();

                return _response.result.Last;
            }
            catch(Exception ex)
            {
                string msg = String.Format("Ticker {0} not found. Make sure the ticker is a valid {1} ticker, e.g. \"{2}\"", ticker, "Bittrex", "BTC-ETH");
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

    class GetTicker
    {
        public class Result
        {
            public double Bid { get; set; }
            public double Ask { get; set; }
            public double Last { get; set; }

        }

        public class RootObject
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Result result { get; set; }
        }
    }
}
