using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1
{
    public static class Database
    {
        public static string ConnectionString = "Server=localhost;Database=db_lista_compras;Uid=root;Pwd=;";
        public static MySqlConnection GetConnection() => new MySqlConnection(ConnectionString);
    }
}