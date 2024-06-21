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
        public bool CreateAccount(string name,string email, string hashedPassword, bool doesUserWantToSell, bool? companyRent)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                if(doesUserWantToSell)
                {
                    if(companyRent==true)
                    {
                        string UserSellQuery = $"INSERT INTO user (name, email, passwordhash, typeuser,typeseller) VALUES ('{name}', '{email}', '{hashedPassword}', 'Seller','Company');";
                        MySqlCommand UserSellCommand = new MySqlCommand(UserSellQuery, connection);
                        UserSellCommand.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        string UserSellQuery = $"INSERT INTO user (name, email, passwordhash, typeuser,typeseller) VALUES ('{name}', '{email}', '{hashedPassword}', 'Seller', 'Personal');";
                        MySqlCommand UserSellCommand = new MySqlCommand(UserSellQuery, connection);
                        UserSellCommand.ExecuteNonQuery();
                        return true;
                    }
                    
                }
                else
                {
                    string UserRentQuery = $"INSERT INTO user (name, email, passwordhash, typeuser) VALUES ('{name}', '{email}', '{hashedPassword}', 'Renter');";
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
    }
}
