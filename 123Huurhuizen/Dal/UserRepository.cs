using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Logic.interfaces;
using Logic.models;

namespace Dal
{
    public class UserRepository : IUserRepository
    {
        private string constring = "Server=127.0.0.1;Port=3306;Database=123huizen;Uid=root;";

        public bool CreateAccount(User user)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                if (user.DoesUserWantToSell)
                {
                    if (user.CompanyRent == true)
                    {
                        string UserSellQuery = $"INSERT INTO user (name, email, passwordhash, typeuser,typeseller) VALUES ('{user.Name}', '{user.Email}', '{user.HashedPassword}', 'Seller','Company');";
                        MySqlCommand UserSellCommand = new MySqlCommand(UserSellQuery, connection);
                        UserSellCommand.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        string UserSellQuery = $"INSERT INTO user (name, email, passwordhash, typeuser,typeseller) VALUES ('{user.Name}', '{user.Email}', '{user.HashedPassword}', 'Seller', 'Personal');";
                        MySqlCommand UserSellCommand = new MySqlCommand(UserSellQuery, connection);
                        UserSellCommand.ExecuteNonQuery();
                        return true;
                    }

                }
                else
                {
                    string UserRentQuery = $"INSERT INTO user (name, email, passwordhash, typeuser) VALUES ('{user.Name}', '{user.Email}', '{user.HashedPassword}', 'Renter');";
                    MySqlCommand UserRentcommand = new MySqlCommand(UserRentQuery, connection);
                    UserRentcommand.ExecuteNonQuery();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CheckIfUserExist(string email, string hashedPassword, out int userId)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string CheckUserExistQuery = $"SELECT COUNT(*) as number, Id FROM user WHERE email = '{email}' AND passwordHash = '{hashedPassword}';";
                MySqlCommand mySqlCommand = new MySqlCommand(CheckUserExistQuery, connection);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    int count = Convert.ToInt32(reader["number"]);
                    if (count > 0)
                    {
                        userId = reader.GetInt32(1);
                        return true;
                    }
                }

            }
            finally { connection.Close(); }
            userId = -1;
            return false;
        }

        public string GetUserName(int id)
        {
            string name = "";
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string CheckUserExistQuery = $"SELECT name FROM user where id ={id}";
                MySqlCommand mySqlCommand = new MySqlCommand(CheckUserExistQuery, connection);
                MySqlDataReader reader = mySqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    name = reader.GetString(0);
                }

            }
            finally { 
                connection.Close(); 
            }

            return name;
        }
    }
}
