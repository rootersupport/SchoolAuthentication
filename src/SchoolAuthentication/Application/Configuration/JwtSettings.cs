namespace Application.Configuration;

public class JwtSettings
{
    public string SecretKey { get; init; }
    public string Issuer { get; init; }
    public double ExpiryInMinutes { get; init; }
}