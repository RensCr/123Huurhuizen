using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.models
{
    public class HouseInformation
    {
        public string Location {  get; set; }
        public double Price {  get; set; }
        public DateTime RentalStart { get; set; }
        public string HouseDescription { get; set; }
        public bool? Rented { get; set; }

        public HouseInformation(string location,double price,DateTime rentalStart,string houseDescription, bool? rented = false) 
        {
            Location = location;
            Price = price;
            RentalStart = rentalStart;
            HouseDescription = houseDescription;
            Rented = rented;
        }
    }
}
