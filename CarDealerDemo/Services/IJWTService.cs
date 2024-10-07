using CarDealerDemo.Helper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarDealerDemo.Services;

public interface IJWTService
{
    Task<string> GenerateTokenAsync(int id);
}

public class JWTService : IJWTService
{
    #region Fields

    private readonly IDealerService _dealerService;
    private readonly JwtOptions _jwtSettings;

    #endregion

    #region Ctor
    public JWTService(IDealerService dealerService,
          IOptions<JwtOptions> jwtSettings)
    {
        _dealerService = dealerService;
        _jwtSettings = jwtSettings.Value;
    }

    #endregion

    #region LocalMethods
    private async Task<Dictionary<string, object>> GetClaimsAsync(int id)
    {
        var dealer = await _dealerService.FindDealerByIdAsync(id);

        if (dealer == null)
        {
            return new Dictionary<string, object>();
        }
        var claims = new Dictionary<string, object>
            {
        { ClaimTypes.Email, dealer.Email },
        { ClaimTypes.Name, dealer.Name },
        { ClaimTypes.NameIdentifier, dealer.ID.ToString() }
    };
        return claims;
    }

    #endregion

    #region Methods
    public async Task<string> GenerateTokenAsync(int id)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = await GetClaimsAsync(id);
            if (claims == null || claims.Count == 0)
                throw new Exception("Claims could not be retrieved for the dealer.");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Claims = claims,
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    #endregion
}
