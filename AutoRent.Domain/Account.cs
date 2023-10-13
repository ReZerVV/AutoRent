namespace AutoRent.Domain;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public AccountRole Role { get; set; }
    public List<AutoRentOrder> Orders { get; set; }
}