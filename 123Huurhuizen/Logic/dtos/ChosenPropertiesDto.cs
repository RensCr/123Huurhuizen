
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

        public ChosenPropertiesDto(int id, int amount, string? name)
        {
            Id = id;
            Amount = amount;
            Name = name;
        }
    }

}
