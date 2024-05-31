using Logic.dtos;

namespace Models
{
    public class HouseViewModel
    {
        public List<House> Houses { get; set; }

        public HouseViewModel(List<House> houses)
        {
            Houses = houses;
        }
    }

}
