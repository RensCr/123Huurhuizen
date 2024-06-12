
namespace Logic.dtos
{
    public class ChosenPropertiesDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string? Name { get; set; } = string.Empty;

        public ChosenPropertiesDto()
        {

        }

        public ChosenPropertiesDto(int id, int quantity, string? name)
        {
            Id = id;
            Amount = quantity;
            Name = name;
        }
    }

}
