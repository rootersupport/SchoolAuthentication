using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Parent;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ParentService: IParentService
{
    private readonly IParentRepository _parentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ParentService> _logger;

    public ParentService(IParentRepository parentRepository, IStudentRepository studentRepository, IMapper mapper, 
        ILogger<ParentService> logger)
    {
        _parentRepository = parentRepository;
        _studentRepository = studentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> AddParentAsync(CreateParentDto createParentDto)
    {
        try
        {
            var existingParent = await _parentRepository.GetByLoginAsync(createParentDto.Login);
            if (existingParent != null && existingParent.IsDeleted == false)
            {
                throw new AlreadyExistsException("Преподаватель с таким логином уже существует.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createParentDto.Password);

            createParentDto.Password = hashedPassword;
        
            var parent = _mapper.Map<Parent>(createParentDto);

            await _parentRepository.InsertAsync(parent);
            
            return parent.Id;
        }
        catch (Exception e) 
        {
            _logger.LogWarning($"Не удалось добавить нового учителя: {e}");
            throw;
        }
    }

    public async Task<Parent> GetParentByIdAsync(Guid id)
    {
        var parent = await _parentRepository.GetByIdAsync(id);
        if (parent is null)
            throw new NotFoundException("Родитель не найден.");

        return parent;
    }

    public async Task<IReadOnlyCollection<Parent>> GetAllParentsAsync()
    {
        var parents = await _parentRepository.GetAllAsync();
        return parents;
    }

    public async Task<Guid> ChangeParentAsync(ChangeParentDto changeParentDto)
    {
        var parent = await _parentRepository.GetByIdAsync(changeParentDto.Id);
        if (parent is null)
            throw new NotFoundException("Родитель не найден.");

        _mapper.Map(changeParentDto, parent);
        await _parentRepository.UpdateAsync(parent);

        return parent.Id;
    }

    public async Task<Guid> DeleteParentAsync(Guid parentId)
    {
        var parent = await _parentRepository.GetByIdAsync(parentId);
        if (parent is null)
            throw new NotFoundException("Родитель не найден.");

        await _parentRepository.DeleteAsync(parentId);
        return parentId;
    }
    
    public async Task<Guid> AddStudentToParentAsync(Guid parentId, Guid studentId)
    {
        var parent = await _parentRepository.GetByIdAsync(parentId);
        if (parent == null || parent.IsDeleted)
            throw new NotFoundException("Родитель не найден или был удален.");

        var student = await _studentRepository.GetByIdAsync(studentId);
        if (student == null)
            throw new NotFoundException("Ученик не найден.");

        parent.AddStudent(student);

        await _parentRepository.UpdateAsync(parent);
        return parent.Id;
    }

}