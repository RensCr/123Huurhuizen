using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class HouseInformationOverview
    {
        public int HouseId { get; set; }
        public string Location { get; set; }
        public double PricePerMonth { get; set; }
        public string HouseInformation { get; set; }
        public DateTime RentalStartTime { get; set; }
        public string SortRenter { get; set; }
        public string RenterName { get; set; }
        public List<Photo> HousePhotos { get; set; }
        public List<ChosenProperties> SelectedProperties { get; set; }

        public HouseInformationOverview(int houseId, string location, double pricePerMonth, string houseInformation, DateTime rentalStartTime, string sortRenter, string renterName, List<Photo> housePhotos, List<ChosenProperties> selectedProperties)
        {
            HouseId = houseId;
            Location = location;
            PricePerMonth = pricePerMonth;
            HouseInformation = houseInformation;
            RentalStartTime = rentalStartTime;
            SortRenter = sortRenter;
            RenterName = renterName;
            HousePhotos = housePhotos;
            SelectedProperties = selectedProperties;
        }
    }
}
