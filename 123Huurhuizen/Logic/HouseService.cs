using Logic.interfaces;
using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class HouseService
    {
        private readonly IHouseRepository _houseRepository;
        public HouseService(IHouseRepository houseRepository) 
        {
            this._houseRepository = houseRepository;
        }

        public List<House> GetAllHouses() => _houseRepository.GetAllHouses();

        public int AddHouse(HouseInformation houseInfo) => _houseRepository.AddHouse(houseInfo);

        public bool AddHousePictures(int HouseId, List<string> Photolinks) => _houseRepository.AddHousePictures(HouseId, Photolinks);

        public bool DeleteHouse(int houseId) => _houseRepository.DeleteHouse(houseId);

        public bool UpdateHouse(int houseId, double rentPerMonth, DateTime availableAt) => _houseRepository.UpdateHouse(houseId, rentPerMonth, availableAt);
    }
}
    