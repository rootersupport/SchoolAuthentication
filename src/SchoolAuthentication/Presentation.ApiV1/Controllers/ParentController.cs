using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Parent;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ApiV1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("apiv1/v{version:apiVersion}/[controller]")]
public class ParentController: ControllerBase
{
    private readonly IParentService _parentService;

    public ParentController(IParentService parentService)
    {
        _parentService = parentService;
    }

    [HttpGet("GetAllParents")]
    public async Task<IActionResult> GetAllParentsAsync()
    {
        return Ok(await _parentService.GetAllParentsAsync());
    }
    
    [HttpGet("GetParentById")]
    public async Task<IActionResult> GetParentByIdAsync([FromQuery] Guid parentId)
    {
        return Ok(await _parentService.GetParentByIdAsync(parentId));
    }
    
    [HttpPost("AddParent")]
    public async Task<IActionResult> AddNewParentAsync([FromBody] CreateParentDto createParentDto)
    {
        return Ok(await _parentService.AddParentAsync(createParentDto));
    }
    
    [HttpPut("ChangeParent")]
    public async Task<IActionResult> ChangeParentAsync([FromBody] ChangeParentDto changeParentDto)
    {
        return Ok(await _parentService.ChangeParentAsync(changeParentDto));
    }

    [HttpDelete("DeleteParent")]
    public async Task<IActionResult> DeleteParentAsync([FromQuery] Guid parentId)
    {
        return Ok(await _parentService.DeleteParentAsync(parentId));
    }

    [HttpPost("AssignStudentToParent")]
    public async Task<IActionResult> AddStudentToParentAsync([FromQuery] Guid parentId, [FromQuery] Guid studentId)
    {
        return Ok(await _parentService.AddStudentToParentAsync(parentId, studentId));
    }
}