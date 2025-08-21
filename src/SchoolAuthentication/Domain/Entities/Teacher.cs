namespace Domain.Entities;

public class Teacher
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Surname { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string PhoneNumber { get; private set; }
    public string BankName { get; private set; }
    public string CardNumber { get; private set; }
    public DateTime DateCreated { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public void MarkAsDeleted() => IsDeleted = true;
    
    public Teacher(
        string name,
        string? surname,
        string login,
        string password,
        string phoneNumber,
        string bankName,
        string cardNumber,
        DateTime dateCreated,
        bool isDeleted)
    {
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Login = login;
        Password = password;
        PhoneNumber = phoneNumber;
        BankName = bankName;
        CardNumber = cardNumber;
        DateCreated = dateCreated;
        IsDeleted = isDeleted;
    }
}