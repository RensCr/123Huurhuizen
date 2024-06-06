using Logic.dtos;

namespace Logic.interfaces
{
    public interface IHouseService
    {
        int AddHouse(HouseInformation houseInfo);
        bool AddHousePictures(int HouseId, List<string> Photolinks);
        bool DeleteHouse(int houseId);
        List<House> GetAllHouses();
        bool UpdateHouse(UpdateHouseDto updateHouseDto);
        public List<Properties> GetAvailableProperties();
    }
}