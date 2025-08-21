using Application.Contracts.DTOs.Authentication;
using Application.Contracts.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ApiV1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("apiv1/v{version:apiVersion}/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _jwtService.LoginAsync(request.Login, request.Password);
        return Ok(result);
    }
}