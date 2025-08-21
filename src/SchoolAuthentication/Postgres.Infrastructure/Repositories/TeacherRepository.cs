using Application.Infrastructure.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres.Infrastructure.Data;

namespace Postgres.Infrastructure.Repositories;

public class TeacherRepository: ITeacherRepository
{
    private readonly ApplicationDbContext _context;

    public TeacherRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Teacher?> GetByIdAsync(Guid id)
    {
        return await _context.Teachers.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task InsertAsync(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid teacherId)
    {
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
        teacher.MarkAsDeleted();
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Teacher?> GetByLoginAsync(string login)
    {
        return await _context.Teachers.Where(u => u.Login == login)
            .FirstOrDefaultAsync();
    }
}