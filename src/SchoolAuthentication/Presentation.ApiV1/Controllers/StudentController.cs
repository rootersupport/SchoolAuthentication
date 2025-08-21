using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ApiV1.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("apiv1/v{version:apiVersion}/[controller]")]
public class StudentController: ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet("GetAllStudents")]
    public async Task<IActionResult> GetAllStudentsAsync()
    {
        return Ok(await _studentService.GetAllStudentsAsync());
    }
    
    [HttpGet("GetStudentById")]
    public async Task<IActionResult> GetStudentByIdAsync([FromQuery] Guid studentId)
    {
        return Ok(await _studentService.GetStudentByIdAsync(studentId));
    }
    
    [HttpPost("AddStudent")]
    public async Task<IActionResult> AddNewStudentAsync([FromBody] CreateStudentDto createStudentDto)
    {
        return Ok(await _studentService.AddStudentAsync(createStudentDto));
    }
    
    [HttpPut("ChangeStudent")]
    public async Task<IActionResult> ChangeStudentAsync([FromBody] ChangeStudentDto changeStudentDto)
    {
        return Ok(await _studentService.ChangeStudentAsync(changeStudentDto));
    }
}