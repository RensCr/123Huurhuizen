using Logic.dtos;

namespace Models
{
    public class HouseInformationViewModel
    {
        public HouseInformationOverviewDto Housenformation{  get; set; }

        public HouseInformationViewModel(HouseInformationOverviewDto housenformation)
        {
            Housenformation = housenformation;
        }
    }
}
