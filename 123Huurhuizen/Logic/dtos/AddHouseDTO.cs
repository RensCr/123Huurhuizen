using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class AddHouseDTO
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Information { get; set; }
    }
}
