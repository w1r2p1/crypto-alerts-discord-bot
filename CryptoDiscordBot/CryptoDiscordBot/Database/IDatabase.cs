using CryptoDiscordBot.Crypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoDiscordBot.Database
{
    interface IDatabase
    {
        void insertAlert(Alert alert);
        List<Alert> getAllAlerts();
    }
}
