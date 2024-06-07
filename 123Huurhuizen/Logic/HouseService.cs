using Logic.dtos;
using Logic.interfaces;


namespace Logic
{
    public class HouseService : IHouseService
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

        public bool UpdateHouse(UpdateHouseDto updateHouseDto) => _houseRepository.UpdateHouse(updateHouseDto);
        public List<Properties> GetAvailableProperties() => _houseRepository.GetAvailableProperties();
        public bool SetHouseProperties(int houseId, List<ChosenProperties> chosenProperties) => _houseRepository.SetHouseProperties(houseId, chosenProperties);
        public HouseInformationOverview GetHouseInformationOverview(int houseId) => _houseRepository.GetHouseInformationOverview(houseId);
        public bool checkIfHouseExist(int id) => _houseRepository.checkIfHouseExist(id);
    }
}
    