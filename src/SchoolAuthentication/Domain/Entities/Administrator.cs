namespace Domain.Entities;

public class Administrator
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Surname { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public DateTime DateCreated { get; private set; }
}