using Logic.dtos;
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
        public List<House> GetAllHouses();
        public bool DeleteHouse(int houseId);
        public bool UpdateHouse(UpdateHouseDto updateHouseDto);
    }
}
