using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace MySQLRepository
{
    public static class MySQLConnection
    {
        public static MySqlConnection MySqlConnectionApplication()
        {
            MySqlConnection conn = null;
            try
            {
                string connectionString = "server=127.0.0.1;port=3306;user id=root;password=root;database=devtest;";
                conn = new MySqlConnection(connectionString);
                //con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConn"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }
    }
}
