using CarDealerDemo.Dto;
using CarDealerDemo.Helper;
using CarDealerDemo.Models;
using System.Security.Claims;

namespace CarDealerDemo.Services;

public class DealerService : IDealerService
{
    #region Fields

    private readonly IDealerRepository _dealerRepository;
    private readonly IPasswordHasher _passwordHasher;

    #endregion

    #region Ctor
    public DealerService(IDealerRepository dealerRepository,
           IPasswordHasher passwordHasher)
    {
        _dealerRepository = dealerRepository;
        _passwordHasher = passwordHasher;
    }

    #endregion
    #region Local Methods

    private async Task<bool> IsDealerExist(string email, string name)
    {
        return await _dealerRepository.IsEmailOrNameExistsAsync(email, name);

    }

    #endregion
    #region Methods
    public async Task<Result<int>> AddDealerAsync(RegisterDealerDto dealerDto)
    {
        if (await IsDealerExist(dealerDto.Email, dealerDto.Name))
            return Result<int>.Failure("The Email Or Name Already Exists");

        var dealer = new Dealer
        {
            Email = dealerDto.Email,
            Name = dealerDto.Name,
            PasswordHash = _passwordHasher.Hash(dealerDto.Password)
        };
        var id = await _dealerRepository.AddDealerAsync(dealer);

        return id > 0
        ? Result<int>.Success(id)
        : Result<int>.Failure("Cannot add the dealer");

    }

    public async Task<Dealer> GetDealerAsync(string name)
    {
        return await _dealerRepository.GetDealerAsync(name);
    }
    public async Task<Dealer> FindDealerByIdAsync(int id)
    {
        return await _dealerRepository.FindDealerByIdAsync(id);
    }
    public bool IsPasswordValid(string password, string hashPassword)
    {
        return _passwordHasher.Verify(password, hashPassword);
    }
    public async Task<bool> UpdateToken(int id, string token)
    {
        return await _dealerRepository.UpdateToken(id, token);
    }

    public int GetDealerIdByClaim(ClaimsPrincipal claims)
    {
        var dealerIdClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
        if (dealerIdClaim == null)
        {
            return -1;
        }
        int.TryParse(dealerIdClaim.Value, out int result);
        return result;

    }

    #endregion
}
