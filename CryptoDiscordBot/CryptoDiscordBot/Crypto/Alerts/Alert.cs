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
        public string Comment { get; set; }
        public Comparison Comparison { get; set; }
        public bool Triggered { get; set; }
        public int Id { get; set; }
        public double CurrentPrice { get; set; }

        public Alert(string ticker, string exchange, double price, Comparison comparison, int id, string comment = "")
        {
            Ticker = ticker;
            Exchange = exchange;
            Price = price;
            Comparison = comparison;
            Triggered = false;
            Id = id;
            Comment = comment;
        }

        public override string ToString()
        {
            string sAlert = $"ID {Id}: {Ticker} {Comparison} {((decimal)(Price)).ToString()} on {Exchange}. Comment: {Comment}";
            return sAlert;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Alert;

            if (item == null)
            {
                return false;
            }

            return item.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

    }

}
