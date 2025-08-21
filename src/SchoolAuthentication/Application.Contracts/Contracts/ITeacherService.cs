using Application.Contracts.DTOs.Teacher;
using Domain.Entities;

namespace Application.Contracts.Contracts;

public interface ITeacherService
{
    public Task<Guid> AddTeacherAsync(CreateTeacherDto createTeacherDto);
    public Task<IReadOnlyCollection<Teacher>> GetAllTeachersAsync();
    public Task<Teacher> GetTeacherByIdAsync(Guid teacherId);
    public Task<Guid> ChangeTeacherAsync(ChangeTeacherDto changeTeacherDto);
    public Task<Guid> DeleteTeacherAsync(Guid teacherId);
}