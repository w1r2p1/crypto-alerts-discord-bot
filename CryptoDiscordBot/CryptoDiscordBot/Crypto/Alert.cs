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
        public Comparison Direction { get; set; }
        public bool Triggered { get; set; }

        public Alert(string ticker, string exchange, double price, Comparison direction)
        {
            Ticker = ticker;
            Exchange = exchange;
            Price = price;
            Direction = direction;
            Triggered = false;
        }

    }

    public enum Comparison
    {
        Above,
        Below
    }
}
