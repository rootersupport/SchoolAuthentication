using Application.Infrastructure.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres.Infrastructure.Data;

namespace Postgres.Infrastructure.Repositories;

public class ParentRepository: IParentRepository
{
    private readonly ApplicationDbContext _context;

    public ParentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Parent?> GetByIdAsync(Guid id)
    {
        return await _context.Parents.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Parent>> GetAllAsync()
    {
        return await _context.Parents.ToListAsync();
    }

    public async Task InsertAsync(Parent parent)
    {
        await _context.Parents.AddAsync(parent);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Parent parent)
    {
        _context.Parents.Update(parent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid parentId)
    {
        var parent = await _context.Parents.FirstOrDefaultAsync(t => t.Id == parentId);
        parent.MarkAsDeleted();
        _context.Parents.Update(parent);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Parent?> GetByLoginAsync(string login)
    {
        return await _context.Parents.Where(u => u.Login == login)
            .FirstOrDefaultAsync();
    }
}