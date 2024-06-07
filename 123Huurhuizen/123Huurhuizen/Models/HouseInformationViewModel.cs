using Logic.dtos;

namespace Models
{
    public class HouseInformationViewModel
    {
        public HouseInformationOverview Housenformation{  get; set; }

        public HouseInformationViewModel(HouseInformationOverview housenformation)
        {
            Housenformation = housenformation;
        }
    }
}
