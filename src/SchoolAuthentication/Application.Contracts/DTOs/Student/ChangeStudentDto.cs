namespace Application.Contracts.DTOs.Student;

public class ChangeStudentDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public string? Surname { get; set; }
    public int Class { get; set; }
    public string? Comment { get; set; }
    public string? TeacherName { get; set; }
}