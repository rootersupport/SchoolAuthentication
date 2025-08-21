using Application.Contracts.DTOs.Student;
using Domain.Entities;

namespace Application.Contracts.Contracts;

public interface IStudentService
{
    public Task<Guid> AddStudentAsync(CreateStudentDto createStudentDto);
    public Task<Student> GetStudentByIdAsync(Guid id);
    public Task<IReadOnlyCollection<Student>> GetAllStudentsAsync();
    public Task<Guid> ChangeStudentAsync(ChangeStudentDto changeStudentDto);
}