using Logic.dtos;

namespace Models
{
    public class HouseOverviewViewModel
    {
        public List<HouseDto> Houses { get; set; }

        public HouseOverviewViewModel(List<HouseDto> houses)
        {
            Houses = houses;
        }
    }

}
