using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CryptoDiscordBot
{
    class Config
    {
        public string DiscordBotToken { get; set; }

        public string DiscordServerId { get; set; }

        public string DiscordChannelId { get; set; }

        public string DatabaseServerUrl { get; set; }

        public int DatabasePort { get; set; }

        public string DatabaseName { get; set; }

        public string DatabaseTable { get; set; }

        public string DatabaseUser { get; set; }

        public string DatabasePassword { get; set; }

        public Config()
        {

        }

        public Config(string discordBotToken, string discordServerId, string discordChannelId, string databaseServerUrl, int databasePort,
                        string databaseName, string databaseTable, string databaseUser, string databasePassword)
        {
            DiscordBotToken = discordBotToken;
            DiscordServerId = discordServerId;
            DiscordChannelId = discordChannelId;
            DatabaseServerUrl = databaseServerUrl;
            DatabasePort = databasePort;
            DatabaseName = databaseName;
            DatabaseTable = databaseTable;
            DatabaseUser = databaseUser;
            DatabasePassword = databasePassword;
        }

        public static Config readConfigFile(string filePath)
        {
            Config config = new Config();
            string[] lines = File.ReadAllLines(filePath);

            config.DiscordBotToken = lines[0].Split('=')[1].Trim();
            config.DiscordServerId = lines[1].Split('=')[1].Trim();
            config.DiscordChannelId = lines[2].Split('=')[1].Trim();
            config.DatabaseServerUrl = lines[3].Split('=')[1].Trim();
            config.DatabasePort = Int32.Parse(lines[4].Split('=')[1].Trim());
            config.DatabaseName = lines[5].Split('=')[1].Trim();
            config.DatabaseTable = lines[6].Split('=')[1].Trim();
            config.DatabaseUser = lines[7].Split('=')[1].Trim();
            config.DatabasePassword = lines[8].Split('=')[1].Trim();

            return config;
        }
    }
}
