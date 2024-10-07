namespace CarDealerDemo.Helper;

public sealed record JwtOptions(
 string Secret,
 string Issuer,
 string Audience,
 bool ValidateIssuer,
 bool ValidateAudience,
 bool ValidateLifeTime,
 bool ValidateIssuerSigningKey,
 int AccessTokenExpireDate,
 int RefreshTokenExpireDate
)
{
    public JwtOptions() : this(
        Secret: string.Empty,
        Issuer: string.Empty,
        Audience: string.Empty,
        ValidateIssuer: false,
        ValidateAudience: false,
        ValidateLifeTime: false,
        ValidateIssuerSigningKey: false,
        AccessTokenExpireDate: 0,
        RefreshTokenExpireDate: 0)
    {
    }
}

