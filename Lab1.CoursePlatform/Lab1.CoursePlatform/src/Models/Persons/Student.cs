namespace Lab1.CoursePlatform.Models.Persons;

public class Student : Person
{
    public string StudentId { get; set; } = Guid.NewGuid().ToString();
    public string PersonalInfo { get; set; }
    
    public Student(string fullName, string personalInfo)
        : base(fullName)
        {
            PersonalInfo = personalInfo;
        }
}