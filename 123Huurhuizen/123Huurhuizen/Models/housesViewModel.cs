using Logic.models;

namespace _123Huurhuizen.Models
{
    public class HouseViewModel
    {
        public List<Photo> HousePhotos { get; set; }
        public List<double> RentPerMonth { get; set; }
        public List<string> AvailableAt { get; set; }
        public List<string> SortRenter { get; set; }

        public HouseViewModel(List<Photo> housePhotos, List<double> rentPerMonth, List<string> availableAt, List<string> sortRenter)
        {
            HousePhotos = housePhotos;
            RentPerMonth = rentPerMonth;
            AvailableAt = availableAt;
            SortRenter = sortRenter;
        }
    }

}
