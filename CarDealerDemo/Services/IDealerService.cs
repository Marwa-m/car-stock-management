using CarDealerDemo.Dto;
using CarDealerDemo.Helper;
using CarDealerDemo.Models;
using System.Security.Claims;

namespace CarDealerDemo.Services;

public interface IDealerService
{
    Task<Dealer> GetDealerAsync(string name);
    int GetDealerIdByClaim(ClaimsPrincipal claims);
    Task<Dealer> FindDealerByIdAsync(int id);
    Task<Result<int>> AddDealerAsync(RegisterDealerDto dealerDto);
    bool IsPasswordValid(string password, string hashPassword);
    Task<bool> UpdateToken(int id, string token);

}
