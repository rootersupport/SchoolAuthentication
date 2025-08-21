using Application.Contracts.DTOs.Parent;
using Application.Contracts.DTOs.Teacher;
using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface IAdministratorRepository
{ 
    Task<Administrator?> GetByLoginAsync(string login);
    Task<Administrator?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<Administrator>> GetAllAsync();
    Task InsertAsync(Administrator administrator);
    Task UpdateAsync(Administrator administrator);
}