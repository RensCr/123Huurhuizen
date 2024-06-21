using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class LoadPropertiesDto
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public string Unit {  get; set; }

        public LoadPropertiesDto(int id, string name,string unit) 
        {
            Id = id;
            Name = name;
            Unit = unit;
        }
    }
}
