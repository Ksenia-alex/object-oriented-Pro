namespace Lab1.CoursePlatform.Models.Persons;

public class Teacher : Person
{
    public string TeacherId { get; set; } = Guid.NewGuid().ToString();
    public string Specialization { get; set; }
    public string Bio  { get; set; }

    public Teacher(string fullName, string specialization)
        : base(fullName)
    {
        Specialization = specialization;
    }
}