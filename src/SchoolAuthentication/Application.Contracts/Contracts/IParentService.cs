using Application.Contracts.DTOs.Parent;
using Domain.Entities;

namespace Application.Contracts.Contracts;

public interface IParentService
{
    public Task<Guid> AddParentAsync(CreateParentDto createParentDto);
    public Task<Parent> GetParentByIdAsync(Guid id);
    public Task<IReadOnlyCollection<Parent>> GetAllParentsAsync();
    public Task<Guid> ChangeParentAsync(ChangeParentDto changeParentDto);
    public Task<Guid> DeleteParentAsync(Guid parentId);

}