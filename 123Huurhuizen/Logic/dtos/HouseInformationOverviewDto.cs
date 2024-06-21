namespace Logic.dtos
{
    public class HouseInformationOverviewDto
    {
        public int HouseId { get; set; }
        public string Location { get; set; }
        public double PricePerMonth { get; set; }
        public string HouseInformation { get; set; }
        public DateTime RentalStartTime { get; set; }
        public string SortRenter { get; set; }
        public string RenterName { get; set; }
        public List<Photo> HousePhotos { get; set; }
        public List<ChosenPropertiesDto> SelectedProperties { get; set; }

        public HouseInformationOverviewDto(int houseId, string location, double pricePerMonth, string houseInformation, DateTime rentalStartTime, string sortRenter, string renterName, List<Photo> housePhotos, List<ChosenPropertiesDto> selectedProperties)
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
