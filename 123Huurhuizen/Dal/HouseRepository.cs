using Logic;
using Logic.dtos;
using Logic.interfaces;
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
                string AddHouseQeury = $"INSERT INTO house (location, price, houseinformation, rentalstartTime, rented, sellerID) VALUES('{house.Location}', '{house.Price}', '{house.HouseDescription}', '{house.RentalStart.ToString("yyyy-MM-dd")}', false, '{house.SellerId}'); SELECT LAST_INSERT_ID();";
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

        public bool AddHousePictures(int HouseId, List<string> Photolinks)
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
        public List<House> GetAllHouses()
        {
            List<House> Houses = new List<House>();

            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string getHouseQuery = "SELECT    p.id,    MIN(p.Photolink) AS first_record,    h.price,   h.rentalStartTime,   h.sellerId,   u.typeSeller, h.id AS HouseId FROM     photos p INNER JOIN   house h ON p.houseid = h.id INNER JOIN     user u ON h.sellerId = u.id WHERE     h.rented = 0 GROUP BY   p.houseid;";
                MySqlCommand command = new MySqlCommand(getHouseQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            List<Photo> Photo = new List<Photo>();
                            int pictureId = Convert.ToInt32(reader["id"]);
                            int sellerId = Convert.ToInt32(reader["sellerId"]);
                            string PhotoLink = reader["first_record"].ToString();
                            double pricePerMonth = Convert.ToDouble(reader["price"]);
                            DateTime rentalStartDate = Convert.ToDateTime(reader["rentalStartTime"]);
                            string formattedStartDate = rentalStartDate.ToString("yyyy-MM-dd");
                            string sortSeller = reader["typeSeller"].ToString();
                            int houseId = Convert.ToInt32(reader["HouseId"]);
                            Photo.Add(new Photo(pictureId, PhotoLink));
                            Houses.Add(new House(houseId, Photo, pricePerMonth, formattedStartDate, sortSeller, sellerId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Houses;
        }
        public bool DeleteHouse(int houseId)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string deleteHouseQuery = $"DELETE FROM house WHERE id = {houseId}";
                MySqlCommand deleteCommand = new MySqlCommand(deleteHouseQuery, connection);
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                int x = 0;
                if (rowsAffected > 0)
                {
                    string DeletehousePhotos = $"DELETE FROM photos WHERE HouseId = {houseId}";
                    MySqlCommand deletePhotosCommand = new MySqlCommand(DeletehousePhotos, connection);
                    x = deletePhotosCommand.ExecuteNonQuery();
                }

                // Als er rijen zijn beïnvloed (d.w.z. verwijderd), geven we true terug, anders false
                return x > 0;
            }
            catch (Exception ex)
            {
                // Handel uitzonderingen op de juiste manier af
                // Hier kun je logica toevoegen om de fout te registreren of te verwerken
                return false; // Geef false terug in geval van een fout
            }
            finally
            {
                connection.Close();
            }
        }
        public bool UpdateHouse(UpdateHouseDto updateHouseDto)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string updateHouseQuery = $"UPDATE house SET price = '{updateHouseDto.RentPerMonth}', rentalStartTime = '{updateHouseDto.AvailableAt.ToString("yyyy-MM-dd")}' WHERE id = {updateHouseDto.HouseId};";
                MySqlCommand updateCommand = new MySqlCommand(updateHouseQuery, connection);
                int rowsAffected = updateCommand.ExecuteNonQuery();

                // Als er rijen zijn beïnvloed (d.w.z. bijgewerkt), geven we true terug, anders false
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handel uitzonderingen op de juiste manier af
                // Hier kun je logica toevoegen om de fout te registreren of te verwerken
                return false; // Geef false terug in geval van een fout
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Properties> GetAvailableProperties()
        {
            List<Properties> properties = new List<Properties>();

            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string getHouseQuery = "SELECT * FROM property;";
                MySqlCommand command = new MySqlCommand(getHouseQuery, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string name = reader.GetString("Name");
                            properties.Add(new Properties(id, name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return properties;
        }
        public bool SetHouseProperties(int houseId, List<ChosenProperties> chosenProperties)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                foreach (var chosenProperty in chosenProperties)
                {
                    string addPropertyQuery = $"INSERT INTO house_properties (House_id , Property_id, Amount) VALUES('{houseId}','{chosenProperty.Id}','{chosenProperty.Quantity}')";
                    MySqlCommand command = new MySqlCommand(addPropertyQuery, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return true;
        }
        public HouseInformationOverview GetHouseInformationOverview(int houseId)
        {
            List<Photo> photos = new List<Photo>();
            List<ChosenProperties> chosenProperties = new List<ChosenProperties>();
            MySqlConnection connection = new MySqlConnection(constring);
            int HouseId = 0;
            string Location = "";
            Double PricePerMonth = 0;
            string HouseInformation = "";
            DateTime RentalStartTime = DateTime.Now;
            string SortRenter = "";
            string RenterName = "";


            try
            {
                connection.Open();
                // Query voor huisinformatie en gebruikersinformatie
                string getHouseInformationQuery = $"SELECT h.id, h.location, h.price, h.houseInformation, h.rentalStartTime, u.typeseller, u.name AS renterName FROM 123huizen.house h JOIN 123huizen.user u ON h.sellerId = u.Id WHERE h.Id = {houseId};";
                MySqlCommand informationCommand = new MySqlCommand(getHouseInformationQuery, connection);

                using (MySqlDataReader reader = informationCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HouseId = reader.GetInt32("id");
                        Location = reader.GetString("location");
                        PricePerMonth = reader.GetDouble("price");
                        HouseInformation = reader.GetString("houseInformation");
                        RentalStartTime = reader.GetDateTime("rentalStartTime");
                        SortRenter = reader.GetString("typeseller");
                        RenterName = reader.GetString("renterName");


                    }
                }

                // Query voor huisfoto's
                string getHousePhotosQuery = $"SELECT p.id AS photoId, p.Photolink FROM 123huizen.photos p WHERE p.HouseId = {houseId};";
                MySqlCommand photosCommand = new MySqlCommand(getHousePhotosQuery, connection);

                using (MySqlDataReader reader = photosCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("photoId");
                        string photoLink = reader.GetString("Photolink");
                        photos.Add(new Photo(id, photoLink));
                    }
                }

                // Query voor eigenschappen van het huis
                string getHousePropertiesQuery = $"SELECT hp.property_id, hp.amount, pr.Name FROM 123huizen.house_properties hp JOIN 123huizen.property pr ON hp.property_id = pr.id WHERE hp.house_id = {houseId};";
                MySqlCommand propertiesCommand = new MySqlCommand(getHousePropertiesQuery, connection);

                using (MySqlDataReader reader = propertiesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int propertyId = reader.GetInt32("property_id");
                        int amount = reader.GetInt32("amount");
                        string propertyName = reader.GetString("Name");
                        chosenProperties.Add(new ChosenProperties(propertyId, amount, propertyName));
                    }
                }

            }
            finally
            {
                connection.Close();
            }

            // Return HouseInformationOverview gevuld met de opgehaalde gegevens
            return new HouseInformationOverview(HouseId, Location, PricePerMonth, HouseInformation, RentalStartTime, SortRenter, RenterName, photos, chosenProperties);
        }
        public bool checkIfHouseExist(int houseId)
        {
            MySqlConnection connection = new MySqlConnection(constring);
            try
            {
                connection.Open();
                string updateHouseQuery = $"SELECT * FROM house WHERE id = {houseId}";
                MySqlCommand updateCommand = new MySqlCommand(updateHouseQuery, connection);

                using (MySqlDataReader reader = updateCommand.ExecuteReader())
                {
                    // Als er rijen worden gevonden in de resultaatset, retourneren we true, anders false
                    return reader.HasRows;
                }

            }
            catch (Exception ex)
            {
                // Handel uitzonderingen op de juiste manier af
                // Hier kun je logica toevoegen om de fout te registreren of te verwerken
                return false; // Geef false terug in geval van een fout
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
