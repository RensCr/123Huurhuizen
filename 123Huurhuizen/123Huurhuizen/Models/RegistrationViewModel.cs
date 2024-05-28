namespace _123Huurhuizen.Models
{
public class RegistrationViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatedPassword { get; set; }
    public bool CheckboxForRent { get; set; }
    public bool? CompanyRent { get; set; }
}
}
