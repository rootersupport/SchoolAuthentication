using System.Globalization;
using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Teacher;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TeacherService: ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AdministratorService> _logger;

    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, ILogger<AdministratorService> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _teacherRepository = teacherRepository;
    }
    
    public async Task<Guid> AddTeacherAsync(CreateTeacherDto createTeacherDto)
    {
        try
        {
            var existingTeacher = await _teacherRepository.GetByLoginAsync(createTeacherDto.Login);
            if (existingTeacher != null && existingTeacher.IsDeleted == false)
            {
                throw new AlreadyExistsException("Преподаватель с таким логином уже существует.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createTeacherDto.Password);

            createTeacherDto.Password = hashedPassword;
        
            var teacher = _mapper.Map<Teacher>(createTeacherDto);

            await _teacherRepository.InsertAsync(teacher);
            
            return teacher.Id;
        }
        catch (Exception e) 
        {
            _logger.LogWarning($"Не удалось добавить нового учителя: {e}");
            throw;
        }
    }

    public async Task<IReadOnlyCollection<Teacher>> GetAllTeachersAsync()
    {
        var existingTeachers = await _teacherRepository.GetAllAsync();

        return existingTeachers;
    }

    public async Task<Teacher> GetTeacherByIdAsync(Guid teacherId)
    {
        var teacherInDb = await _teacherRepository.GetByIdAsync(teacherId);
        
        if (teacherInDb is null)
        {
            throw new NotFoundException("Такого преподавтеля не существует.");
        }

        return teacherInDb;
    }
    
    public async Task<Guid> ChangeTeacherAsync(ChangeTeacherDto changeTeacherDto)
    {
        var teacherInDb = await _teacherRepository.GetByIdAsync(changeTeacherDto.Id);

        if (teacherInDb is null)
        {
            throw new NotFoundException("Такого преподавтеля не существует.");
        }

        _mapper.Map(changeTeacherDto, teacherInDb);

        await _teacherRepository.UpdateAsync(teacherInDb);

        return teacherInDb.Id;
    }

    public async Task<Guid> DeleteTeacherAsync(Guid teacherId)
    {
        var teacherInDb = await _teacherRepository.GetByIdAsync(teacherId);

        if (teacherInDb is null)
            throw new NotFoundException("Такого преподавтеля не существует.");

        await _teacherRepository.DeleteAsync(teacherId);

        return teacherInDb.Id;
    }
}