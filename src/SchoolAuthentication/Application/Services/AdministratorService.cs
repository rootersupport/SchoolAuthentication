using Application.Contracts.Contracts;
using Application.Contracts.DTOs.Administrator;
using Application.Contracts.DTOs.Teacher;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AdministratorService: IAdministratorService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AdministratorService> _logger;

    public AdministratorService(IAdministratorRepository administratorRepository, IMapper mapper, ILogger<AdministratorService> logger)
    {
        _logger = logger;
        _mapper = mapper;
        _administratorRepository = administratorRepository;
    }
    
    public async Task<Guid> ChangeAdministratorAsync(ChangeAdministratorDto changeAdministratorDto)
    {
        var administratorInDb = await _administratorRepository.GetByIdAsync(changeAdministratorDto.Id);

        if (administratorInDb is null)
        {
            throw new NotFoundException("Такого преподавтеля не существует.");
        }

        _mapper.Map(changeAdministratorDto, administratorInDb);

        await _administratorRepository.UpdateAsync(administratorInDb);

        return administratorInDb.Id;
    }
}