using Domain.Entities;

namespace Application.Contracts.DTOs.Parent;

public class CreateParentDto
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
}