using Application.Contracts.DTOs.Administrator;
using Application.Contracts.DTOs.Teacher;

namespace Application.Contracts.Contracts;

public interface IAdministratorService
{
    public Task<Guid> ChangeAdministratorAsync(ChangeAdministratorDto changeAdministratorDto);
}