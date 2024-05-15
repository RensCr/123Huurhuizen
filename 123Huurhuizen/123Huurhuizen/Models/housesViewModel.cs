using Logic.models;

namespace _123Huurhuizen.Models
{
    public class HouseViewModel
    {
        public List<House> houses { get; set; }

        public HouseViewModel(List<House> houses)
        {
            this.houses = houses;
        }
    }

}
