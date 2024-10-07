using CarDealerDemo.Models;

public interface IDealerRepository
{
    Task<bool> IsEmailOrNameExistsAsync(string email, string name);
    Task<Dealer> GetDealerByEmailAsync(string email);
    Task<Dealer> GetDealerAsync(string name);
    Task<int> AddDealerAsync(Dealer dealer);
    Task<Dealer> FindDealerByIdAsync(int id);
    Task<bool> UpdateToken(int id, string token);
}
