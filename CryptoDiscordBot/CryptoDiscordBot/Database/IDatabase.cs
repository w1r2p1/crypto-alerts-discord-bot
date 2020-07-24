using CryptoDiscordBot.Crypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoDiscordBot.Database
{
    interface IDatabase
    {
        void InsertAlert(Alert alert);
        void DeleteAlert(Alert alert);
        List<Alert> GetAllAlerts();
    }
}
