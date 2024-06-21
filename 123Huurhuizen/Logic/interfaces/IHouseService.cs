using Logic.dtos;

namespace Logic.interfaces
{
    public interface IHouseService
    {
        int AddHouse(AddHouseInformationDto houseInfo);
        bool AddHousePictures(int HouseId, List<string> Photolinks);
        bool DeleteHouse(int houseId);
        List<HouseDto> GetAllHouses();
        bool UpdateHouse(UpdateHouseDto updateHouseDto);
        public List<LoadPropertiesDto> GetAvailableProperties();
        public bool SetHouseProperties(int houseId, List<ChosenPropertiesDto> chosenProperties);
        public HouseInformationOverviewDto GetHouseInformationOverview(int houseId);
        public bool checkIfHouseExist(int id);
    }
}