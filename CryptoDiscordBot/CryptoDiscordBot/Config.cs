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

        public bool DatabaseCredentialsSet { get; set; }

        public Config()
        {

        }

        public Config(string discordBotToken, string discordServerId, string discordChannelId, string databaseServerUrl, int databasePort,
                        string databaseName, string databaseTable, string databaseUser, string databasePassword, bool databaseCredentialsSet)
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
            DatabaseCredentialsSet = databaseCredentialsSet;
        }

        public static Config readConfigFile(string filePath)
        {
            Config config = new Config();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length != 3 && lines.Length != 9)
                throw new ArgumentOutOfRangeException("Incorrect number of parameters in config file.");

            config.DiscordBotToken = lines[0].Split('=')[1].Trim();
            config.DiscordServerId = lines[1].Split('=')[1].Trim();
            config.DiscordChannelId = lines[2].Split('=')[1].Trim();

            if (lines.Length == 9)
            {
                config.DatabaseServerUrl = lines[3].Split('=')[1].Trim();
                string dbPort = lines[4].Split('=')[1].Trim();
                config.DatabaseName = lines[5].Split('=')[1].Trim();
                config.DatabaseTable = lines[6].Split('=')[1].Trim();
                config.DatabaseUser = lines[7].Split('=')[1].Trim();
                config.DatabasePassword = lines[8].Split('=')[1].Trim();

                if(!String.IsNullOrEmpty(config.DatabaseServerUrl) && !String.IsNullOrEmpty(dbPort) && !String.IsNullOrEmpty(config.DatabaseName)
                    && !String.IsNullOrEmpty(config.DatabaseName) && !String.IsNullOrEmpty(config.DatabaseTable) && !String.IsNullOrEmpty(config.DatabaseUser)
                    && !String.IsNullOrEmpty(config.DatabasePassword))
                {
                    config.DatabasePort = Int32.Parse(dbPort);
                    config.DatabaseCredentialsSet = true;
                }
                else
                {
                    config.DatabaseCredentialsSet = false;
                }
            }

            return config;
        }
    }
}
