using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace N01432018_Assignment3_MVC.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }

        private static string Password { get { return "root"; } }

        private static string Database { get { return "schooldb"; } }

        private static string Server { get { return "localhost"; } }
        
        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server 
                    + "; user = " + User 
                    + "; database = " + Database 
                    + "; port = " + Port
                    + "; Password = " + Password;
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}