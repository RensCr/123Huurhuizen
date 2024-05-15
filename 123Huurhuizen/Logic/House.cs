using Logic.interfaces;
using Logic.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class House
    {
        public int GiveHouseInformationToDal(HouseInformation house,IHouseRepository houseRepository)
        {
            int CreatedHouseID = houseRepository.AddHouse(house);
            return CreatedHouseID;
        }
        public bool GiveHousePicturesToDal(int houseID,List<string>Photolinks, IHouseRepository houseRepository)
        { 
            return houseRepository.AddHousePictures(houseID,Photolinks);
        }
    }
}
