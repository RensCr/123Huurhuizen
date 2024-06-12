using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class AddHouseInformationDto
    {
        public int SellerId { get; set; }
        public string Location { get; set; }
        public double Price { get; set; }
        public DateTime RentalStart { get; set; }
        public string HouseDescription { get; set; }
        public bool? Rented { get; set; }

        public AddHouseInformationDto(int sellerId, string location, double price, DateTime rentalStart, string houseDescription, bool? rented = false)
        {
            SellerId = sellerId;
            Location = location;
            Price = price;
            RentalStart = rentalStart;
            HouseDescription = houseDescription;
            Rented = rented;
        }
    }
}
