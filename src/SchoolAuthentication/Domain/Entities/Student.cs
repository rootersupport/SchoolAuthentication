namespace Domain.Entities;

public class Student
{ 
    public Guid Id { get; private set; }
    public int Number { get; private set; }
    public string Name { get; private set; }
    public string? Surname { get; private set; }
    public int Class { get; private set; }
    public Guid? ParentId { get; private set; }
    public DateTime DateCreated { get; private set; }
    public string? Comment { get; private set; }
    public string? TeacherName { get; private set; }
    public bool IsDeleted { get; private set; }
    
    public Student(int number, string name, string? surname, int @class, DateTime dateCreated, string? comment, string? teacherName)
    {
        Id = Guid.NewGuid();
        Number = number;
        Name = name;
        Surname = surname;
        Class = @class;
        ParentId = null;
        DateCreated = dateCreated;
        Comment = comment;
        TeacherName = teacherName;
        IsDeleted = false;
    }
    
    public void SetParent(Parent parent)
    {
        parent = parent ?? throw new ArgumentNullException(nameof(parent));
        ParentId = parent.Id;
    }
}