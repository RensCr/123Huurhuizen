using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class House
    {
        public int HouseId { get; set; }
        public List<Photo> HousePhotos { get; set; }
        public double RentPerMonth { get; set; }
        public string AvailableAt { get; set; }
        public string SortRenter { get; set; }
        public int SellerId { get; set; }
        public House(int houseId, List<Photo> housePhotos, double rentPerMonth, string availableAt, string sortRenter, int sellerId)
        {
            HouseId = houseId;
            HousePhotos = housePhotos;
            RentPerMonth = rentPerMonth;
            AvailableAt = availableAt;
            SortRenter = sortRenter;
            SellerId = sellerId;
        }
    }

}
