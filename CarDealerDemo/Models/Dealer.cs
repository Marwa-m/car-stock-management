namespace CarDealerDemo.Models;

public class Dealer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Token { get; set; } = string.Empty;
}

