using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ApiV1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("apiv1/v{version:apiVersion}/[controller]")]
public class TeacherController: ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet("GetAllTeachers")]
    public async Task<IActionResult> GetAllTeachersAsync()
    {
        return Ok(await _teacherService.GetAllTeachersAsync());
    }
    
    [HttpGet("GetTeacherById")]
    public async Task<IActionResult> GetTeacherByIdAsync([FromQuery] Guid teacherId)
    {
        return Ok(await _teacherService.GetTeacherByIdAsync(teacherId));
    }
    
    [HttpPost("AddTeacher")]
    public async Task<IActionResult> AddNewTeacherAsync([FromBody] CreateTeacherDto createTeacherDto)
    {
        return Ok(await _teacherService.AddTeacherAsync(createTeacherDto));
    }
    
    [HttpPut("ChangeTeacher")]
    public async Task<IActionResult> ChangeTeacherAsync([FromBody] ChangeTeacherDto changeTeacherDto)
    {
        return Ok(await _teacherService.ChangeTeacherAsync(changeTeacherDto));
    }

    [HttpDelete("DeleteTeacher")]
    public async Task<IActionResult> DeleteTeacherAsync([FromQuery] Guid teacherId)
    {
        return Ok(await _teacherService.DeleteTeacherAsync(teacherId));
    }
}