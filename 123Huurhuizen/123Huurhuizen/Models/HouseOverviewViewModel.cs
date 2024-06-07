using Logic.dtos;

namespace Models
{
    public class HouseOverviewViewModel
    {
        public List<House> Houses { get; set; }

        public HouseOverviewViewModel(List<House> houses)
        {
            Houses = houses;
        }
    }

}
