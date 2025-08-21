using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Administrator;
using Application.Contracts.DTOs.Teacher;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ApiV1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("apiv1/v{version:apiVersion}/[controller]")]
public class AdministratorController: ControllerBase
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }
    
    [HttpPut("ChangeAdministrator")]
    public async Task<IActionResult> ChangeAdministratorAsync([FromBody] ChangeAdministratorDto changeAdministratorDto)
    {
        return Ok(await _administratorService.ChangeAdministratorAsync(changeAdministratorDto));
    }
}