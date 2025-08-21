using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Postgres.Infrastructure.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<Administrator> Administrators { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
    
    public DbSet<Parent> Parents { get; set; }
    
    public DbSet<Student> Students { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(t => t.DateCreated)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
            
        });
        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(t => t.DateCreated)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            entity.Property(u => u.IsDeleted).HasDefaultValue(false);
        });
        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(t => t.DateCreated)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            entity.Property(u => u.IsDeleted).HasDefaultValue(false);
        });
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(t => t.DateCreated)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            entity.Property(u => u.IsDeleted).HasDefaultValue(false);
        });
        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var response = await base.SaveChangesAsync(cancellationToken);
        return response;
    }
}