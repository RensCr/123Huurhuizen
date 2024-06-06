using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class AddHouseInformation
    {
        public List<Properties> Properties { get; set; }
        public AddHouseInformation(List<Properties> properties) 
        {
            Properties = properties;
        }
    }
}
