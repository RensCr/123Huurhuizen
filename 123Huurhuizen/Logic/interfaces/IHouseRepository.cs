using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.interfaces
{
    public interface IHouseRepository
    {
        public int AddHouse(HouseInformation house);
        public bool AddHousePictures(int HouseId, List<string> Photolinks);
        public List<Photo> GetFirstHousePicture();
        public List<double> GetRentPricePerMonth();
        public List<string> GetStartRentedDays();
        public List<string> GetSortRenter();
    }
}
