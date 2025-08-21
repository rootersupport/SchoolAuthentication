using Domain.Entities;

namespace Application.Contracts.DTOs.Parent;

public class ChangeParentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
}