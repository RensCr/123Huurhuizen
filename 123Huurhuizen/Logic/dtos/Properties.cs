using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public  class Properties
    {
        public int Id { get; set; }   
        public string Name { get; set; }

        public Properties(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
