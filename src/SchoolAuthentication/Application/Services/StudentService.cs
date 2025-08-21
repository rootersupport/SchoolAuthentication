using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Student;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class StudentService: IStudentService
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IMapper mapper, IStudentRepository studentRepository, ILogger<StudentService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _studentRepository = studentRepository;
    }
    
    public async Task<Guid> AddStudentAsync(CreateStudentDto createStudentDto)
    {
        var student = _mapper.Map<Student>(createStudentDto);

        await _studentRepository.InsertAsync(student);

        return student.Id;
    }

    public async Task<Student> GetStudentByIdAsync(Guid studentId)
    {
        var studentInDb = await _studentRepository.GetByIdAsync(studentId);

        if (studentInDb is null)
            throw new NotFoundException("Такого студента не существует.");

        return studentInDb;
    }

    public async Task<IReadOnlyCollection<Student>> GetAllStudentsAsync()
    {
        var existingStudents = await _studentRepository.GetAllAsync();
        return existingStudents;
    }

    public async Task<Guid> ChangeStudentAsync(ChangeStudentDto changeStudentDto)
    {
        var studentInDb = await _studentRepository.GetByIdAsync(changeStudentDto.Id);
        
        if (studentInDb is null)
            throw new NotFoundException("Такого студента не существует.");

        _mapper.Map(changeStudentDto, studentInDb);

        await _studentRepository.UpdateAsync(studentInDb);

        return studentInDb.Id;
    }
}