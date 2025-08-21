using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface IStudentRepository
{
    public Task<Student?> GetByIdAsync(Guid id);
    public Task<IReadOnlyCollection<Student>> GetAllAsync();
    public Task InsertAsync(Student student);
    public Task UpdateAsync(Student student);
    public Task DeleteAsync(Guid studentId);
}