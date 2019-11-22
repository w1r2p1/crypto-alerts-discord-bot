using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoDiscordBot.Crypto
{
    class Alert
    {
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public double Price { get; set; }
        public Comparison Comparison { get; set; }
        public bool Triggered { get; set; }

        public Alert(string ticker, string exchange, double price, Comparison comparison)
        {
            Ticker = ticker;
            Exchange = exchange;
            Price = price;
            Comparison = comparison;
            Triggered = false;
        }

    }

    public enum Comparison
    {
        Above,
        Below
    }
}
