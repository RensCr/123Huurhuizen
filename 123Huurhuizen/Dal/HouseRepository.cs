using Logic;
using Logic.interfaces;
using Logic.models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class HouseRepository : IHouseRepository
    {
        private string constring = "Server=127.0.0.1;Port=3306;Database=123huizen;Uid=root;";
        public int AddHouse(HouseInformation house)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string AddHouseQeury = $"INSERT INTO house (location, price, houseinformation, rentalstartTime, rented) VALUES('{house.Location}', '{house.Price}', '{house.HouseDescription}', '{house.RentalStart.ToString("yyyy-MM-dd")}', false); SELECT LAST_INSERT_ID();";
                MySqlCommand UserSellCommand = new MySqlCommand(AddHouseQeury, connection);
                int newHouseId = Convert.ToInt32(UserSellCommand.ExecuteScalar());
                return newHouseId;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                return -1; // Return a default value or throw an exception
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddHousePictures(int HouseId,List<string>Photolinks)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                foreach (var photolink in Photolinks)
                {
                    string addPhotoLinkQuery = $"INSERT INTO photos(HouseId,Photolink)Values('{HouseId}','{photolink}')";
                    MySqlCommand command = new MySqlCommand(addPhotoLinkQuery, connection);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Photo> GetFirstHousePicture()
        {
            MySqlConnection connection = new MySqlConnection(constring);
            List<Photo> housePicture = new List<Photo> ();
            try
            {
                connection.Open();
                string GetHousePicturesQuery = "SELECT p.id, MIN(p.Photolink) AS first_record FROM photos p INNER JOIN house h ON p.houseid = h.id WHERE h.rented = 0 GROUP BY p.houseid;";
                MySqlCommand command = new MySqlCommand(GetHousePicturesQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pictureId = Convert.ToInt32(reader["id"]);
                        string pictureLink = reader["first_record"].ToString();
                        housePicture.Add(new Photo (pictureId,pictureLink));
                    }
                }
            }
            catch(Exception ex)
            {
                return new  List<Photo>();
            }
            finally
            {
                connection.Close();
            }
            return housePicture;
        }
        public List<double> GetRentPricePerMonth()
        {
            List<double> prices = new List<double>();
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string getHousePricesQuery = "SELECT price FROM house WHERE rented = 0;";
                MySqlCommand command = new MySqlCommand(getHousePricesQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        double pricePerMonth = Convert.ToDouble(reader["price"]);
                        prices.Add(pricePerMonth);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception, log it, or rethrow if necessary
            }
            finally
            {
                connection.Close();
            }
            return prices;
        }
        public List<string> GetStartRentedDays()
        {
            List<string> StartDate = new List<string>(); // Hier veranderen we de lijst naar een lijst van strings
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string getHousePricesQuery = "SELECT rentalStartTime FROM house WHERE rented = 0;";
                MySqlCommand command = new MySqlCommand(getHousePricesQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["rentalStartTime"] != DBNull.Value)
                        {
                            DateTime rentalStartDate = Convert.ToDateTime(reader["rentalStartTime"]);
                            string formattedStartDate = rentalStartDate.ToString("yyyy-MM-dd");
                            StartDate.Add(formattedStartDate); // Hier voegen we de geformatteerde datum toe aan de lijst van strings
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Behandel de uitzondering, log deze of gooi deze opnieuw indien nodig
            }
            finally
            {
                connection.Close();
            }
            return StartDate;
        }
        public List<string> GetSortRenter()//Todo: geef userId mee aan het aanmaken van een huis.
        {
            List<string> SortRenter = new List<string>(); // Hier veranderen we de lijst naar een lijst van strings
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string getHousePricesQuery = "SELECT price FROM house WHERE rented = 0;";
                MySqlCommand command = new MySqlCommand(getHousePricesQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            SortRenter.Add("TestRenter");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Behandel de uitzondering, log deze of gooi deze opnieuw indien nodig
            }
            finally
            {
                connection.Close();
            }
            return SortRenter;
        }


    }
}
