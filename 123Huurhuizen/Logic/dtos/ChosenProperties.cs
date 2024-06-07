using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class ChosenProperties
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? Name { get; set; } = string.Empty;

        // Parameterloze constructor toevoegen
        public ChosenProperties()
        {
        }

        public ChosenProperties(int id, int quantity, string? name)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
        }
    }

}
