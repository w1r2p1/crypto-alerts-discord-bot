﻿using System;
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

        public async Task<double> getPrice(string ticker)
        {
            throw new NotImplementedException();
        }

        public async Task<double> getTicker(string ticker)
        {
            // DeserializeObject throws an exception when wrong ticker due to null Result value, need to fix
            string urlExtension = String.Format("public/getticker?market={0}", ticker);
            string url = String.Concat(apiEndpoint, urlExtension);

            string response = await GetAsync(url);

            var _response = JsonConvert.DeserializeObject<GetTicker.RootObject>(response);

            return _response.result.Last;
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
