using CryptoDiscordBot.Crypto;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace CryptoDiscordBot.Database
{
    class MySql : IDatabase
    {
        private string _server;
        private int _port;
        private string _database;
        private string _table;
        private string _username;
        private string _password;

        private string connectionString;
        private MySqlConnection connection;

        public MySql(string server, int port, string database, string table, string username, string password)
        {
            _server = server;
            _port = port;
            _database = database;
            _table = table;
            _username = username;
            _password = password;

            connectionString = String.Format("server={0};user={1};database={2};port={3};password={4}", server, username, database, port, password);
            connection = new MySqlConnection(connectionString);
        }

        public void Connect()
        {
            connection.Open();
        }

        public void InsertAlert(Alert alert)
        {
            List<string> fields = new List<string>() { "id", "ticker", "exchange", "price", "comparison", "userComment" };
            List<string> values = new List<string>() { alert.Id.ToString(), alert.Ticker, alert.Exchange, alert.Price.ToString(), alert.Comparison.ToString(), alert.Comment };
            string sql = generateInsertStatement(_table, fields, values);

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void DeleteAlert(Alert alert)
        {
            string sql = generateDeleteStatement(_table, "id", alert.Id.ToString());

            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public List<Alert> GetAllAlerts()
        {
            List<Alert> alerts = new List<Alert>();

            string sql = generateSelectQuery(new List<string> { "*" }, _table);
            MySqlCommand command = new MySqlCommand(sql, connection);

            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    int id = Int32.Parse(reader[0].ToString());
                    string ticker = reader[1].ToString();
                    string exchange = reader[2].ToString();
                    double price = Double.Parse(reader[3].ToString());
                    Comparison comparison = (Comparison)Enum.Parse(typeof(Comparison), reader[4].ToString());
                    string comment = reader[5].ToString();

                    Alert alert = new Alert(ticker, exchange, price, comparison, id, comment);
                    alerts.Add(alert);
                }
            }

            return alerts;
        }


        private string generateSelectQuery(List<string> select, string fromTable)
        {
            string query = "SELECT ";
            foreach(string s in select)
            {
                query += s + ", ";
            }
            query = query.Remove(query.Length - 2);
            query += "FROM " + fromTable;

            return query;
        }

        private string generateInsertStatement(string table, List<string> fields, List<string> values)
        {
            string statement = "INSERT INTO " + table + " (";
            foreach (string field in fields)
            {
                statement += field + ", ";
            }

            statement = statement.Remove(statement.Length - 2);
            statement += ") VALUES (";

            foreach (string value in values)
            {
                statement += String.Format("'{0}', ", value);
            }

            statement = statement.Remove(statement.Length - 2);
            statement += ")";

            return statement;
        }

        private string generateDeleteStatement(string table, string whereField, string whereValue)
        {
            string statement = String.Format("DELETE FROM {0} WHERE {1}='{2}'", table, whereField, whereValue);
            return statement;
        }

    }
}
