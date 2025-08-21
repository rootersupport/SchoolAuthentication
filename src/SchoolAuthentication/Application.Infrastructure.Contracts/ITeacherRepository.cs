using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface ITeacherRepository
{
    public Task<Teacher?> GetByIdAsync(Guid id);
    public Task<IReadOnlyCollection<Teacher>> GetAllAsync();
    public Task InsertAsync(Teacher teacher);
    public Task UpdateAsync(Teacher teacher);
    public Task DeleteAsync(Guid teacherId);
    public Task<Teacher?> GetByLoginAsync(string login);
}