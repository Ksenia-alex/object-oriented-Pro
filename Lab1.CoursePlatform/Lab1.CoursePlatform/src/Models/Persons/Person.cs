namespace Lab1.CoursePlatform.Models.Persons;

public abstract class Person
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    
    protected Person(string fullName)
    {
        FullName = fullName;
    }
}