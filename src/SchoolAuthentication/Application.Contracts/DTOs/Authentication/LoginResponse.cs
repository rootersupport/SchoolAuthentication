namespace Application.Contracts.DTOs.Authentication;

public class LoginResponse
{
    public string Token { get; set; }
    public string Role { get; set; }
    public Guid Id { get; set; }
}