using Application.Infrastructure.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Postgres.Infrastructure.Data;

namespace Postgres.Infrastructure.Repositories;

public class StudentRepository: IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Student?> GetByIdAsync(Guid id)
    {
        return await _context.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task InsertAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid studentId)
    {
        throw new NotImplementedException();
    }
}