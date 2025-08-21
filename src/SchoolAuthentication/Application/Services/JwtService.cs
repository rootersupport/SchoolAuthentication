using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Configuration;
using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Authentication;
using Application.Infrastructure.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService: IJwtService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IParentRepository _parentRepository;
    private readonly JwtSettings _jwtSettings;

    public JwtService(IAdministratorRepository administratorRepository, ITeacherRepository teacherRepository,
        IParentRepository parentRepository, IOptions<JwtSettings> jwtOptions)
    {
        _administratorRepository = administratorRepository;
        _teacherRepository = teacherRepository;
        _parentRepository = parentRepository;
        _jwtSettings = jwtOptions.Value;
    }
    
    public async Task<LoginResponse> LoginAsync(string login, string password)
    {
        // Пытаемся найти пользователя в каждой таблице
        var admin = await _administratorRepository.GetByLoginAsync(login);
        if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.Password)) 
            return await CreateToken(admin.Id, "Administrator");

        var teacher = await _teacherRepository.GetByLoginAsync(login);
        if (teacher != null && BCrypt.Net.BCrypt.Verify(password, teacher.Password)) 
            return await CreateToken(teacher.Id, "Teacher");

        var parent = await _parentRepository.GetByLoginAsync(login);
        if (parent != null && BCrypt.Net.BCrypt.Verify(password, parent.Password)) 
            return await CreateToken(parent.Id, "Parent");

        throw new UnauthorizedAccessException("Неверный логин или пароль.");
    }

    private async Task<LoginResponse> CreateToken(Guid id, string role)
    {
        var token = await GenerateToken(id, role);
        return new LoginResponse
        {
            Token = token,
            Role = role,
            Id = id
        };
    }
    
    public async Task<string> GenerateToken(Guid id, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier, id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}