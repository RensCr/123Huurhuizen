using Logic.models;

namespace _123Huurhuizen.Models
{
    public class HouseViewModel
    {
        public List<House> Houses { get; set; }

        public HouseViewModel(List<House> houses)
        {
            this.Houses = houses;
        }
    }

}
