using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class UpdateHouseDto
    {
        public int HouseId { get; set; }
        public double RentPerMonth { get; set; }
        public DateTime AvailableAt { get; set; }
        public UpdateHouseDto(int houseId,double rentPerMonth, DateTime availableAt) 
        { 
            HouseId = houseId;
            RentPerMonth = rentPerMonth;
            AvailableAt = availableAt;
        }
    }

}
