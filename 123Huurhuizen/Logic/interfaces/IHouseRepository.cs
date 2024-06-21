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
        public int AddHouse(AddHouseInformationDto house);
        public bool AddHousePictures(int HouseId, List<string> Photolinks);
        public List<HouseDto> GetAllHouses();
        public bool DeleteHouse(int houseId);
        public bool UpdateHouse(UpdateHouseDto updateHouseDto);
        public List<LoadPropertiesDto> GetAvailableProperties();
        public bool SetHouseProperties(int houseId, List<ChosenPropertiesDto> chosenProperties);
        public HouseInformationOverviewDto GetHouseInformationOverview(int houseId);
        public bool checkIfHouseExist(int houseId);
    }
}
