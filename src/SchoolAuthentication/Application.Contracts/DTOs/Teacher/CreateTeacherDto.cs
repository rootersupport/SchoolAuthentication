namespace Application.Contracts.DTOs.Teacher;

public class CreateTeacherDto
{
    public string Name { get; set; }
    public string? Surname { get; set; }
    public string Login { get; set; }
    public string Password { get;  set; }
    public string PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsDeleted { get; set; }
    public string BankName { get; set; }
    public string CardNumber { get; set; }
}