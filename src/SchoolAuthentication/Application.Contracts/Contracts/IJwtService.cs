using Application.Contracts.DTOs.Authentication;

namespace Application.Contracts.Contracts;

public interface IJwtService
{
    public Task<LoginResponse> LoginAsync(string login, string password);
    public Task<string> GenerateToken(Guid id, string role);

}