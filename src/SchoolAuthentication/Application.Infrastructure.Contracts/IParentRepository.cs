using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface IParentRepository
{
    public Task<Parent?> GetByIdAsync(Guid id);
    public Task<IReadOnlyCollection<Parent>> GetAllAsync();
    public Task InsertAsync(Parent parent);
    public Task UpdateAsync(Parent parent);
    public Task DeleteAsync(Guid parentId);
    public Task<Parent?> GetByLoginAsync(string login);
}