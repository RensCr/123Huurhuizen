namespace _123Huurhuizen.Models
{
    public class AddHouseViewModel
{
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string Information { get; set; }
        public List<IFormFile> photos { get; set; }
    }
}
