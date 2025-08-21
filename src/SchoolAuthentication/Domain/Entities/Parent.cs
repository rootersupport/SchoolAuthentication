namespace Domain.Entities;

public class Parent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Surname { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime DateCreated { get; private set; }
    public bool IsDeleted { get; private set; }
    public ICollection<Student> Students { get; private set; } = new List<Student>();
    public void MarkAsDeleted() => IsDeleted = true;
    
    public Parent(string name, string? surname, string login, string password, string phoneNumber, DateTime dateCreated)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Login = login;
        Password = password;
        PhoneNumber = phoneNumber;
        DateCreated = dateCreated;
        IsDeleted = false;
    }
}