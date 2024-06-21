using Logic.dtos;

namespace Models
{
    public class AddHouseInformationViewModel
    {
        public List<LoadPropertiesDto> AvailableProperties { get; set; }
        public AddHouseInformationViewModel(List<LoadPropertiesDto> availableProperties) 
        {
            AvailableProperties = availableProperties;
        }

    }
}
