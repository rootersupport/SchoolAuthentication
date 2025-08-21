using Application.Contracts.DTOs.Parent;
using Application.Contracts.DTOs.Teacher;
using Application.Infrastructure.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres.Infrastructure.Data;

namespace Postgres.Infrastructure.Repositories;

public class AdministratorRepository: IAdministratorRepository
{
    private readonly ApplicationDbContext _context;

    public AdministratorRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Administrator?> GetByLoginAsync(string login)
    {
        return await _context.Administrators.Where(u => u.Login == login)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Administrator?> GetByIdAsync(Guid id)
    {
        return await _context.Administrators.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Administrator>> GetAllAsync()
    {
        return await _context.Administrators.ToListAsync();
    }

    public async Task InsertAsync(Administrator administrator)
    {
        await _context.Administrators.AddAsync(administrator);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Administrator administrator)
    {
        _context.Administrators.Update(administrator);
        await _context.SaveChangesAsync();
    }
}