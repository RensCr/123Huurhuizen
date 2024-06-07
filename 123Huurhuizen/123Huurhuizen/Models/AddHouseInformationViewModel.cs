using Logic.dtos;

namespace Models
{
    public class AddHouseInformationViewModel
    {
        public List<Properties> AvailableProperties { get; set; }
        public AddHouseInformationViewModel(List<Properties> availableProperties) 
        {
            AvailableProperties = availableProperties;
        }

    }
}
