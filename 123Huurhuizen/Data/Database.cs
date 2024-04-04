using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace Data
{
    public class Database
    {
        private string constring = "Server=127.0.0.1;Port=3306;Database=123huizen;Uid=root;";
        public bool CreateAccount(string name,string email, string hashedPassword)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();

                string query = $"INSERT INTO user (name, email, passwordhash, typeuser) VALUES ('{name}', '{email}', '{hashedPassword}', 'regular');";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
