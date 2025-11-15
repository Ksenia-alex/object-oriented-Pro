using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Models.Courses;

public abstract class Course
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Description { get; set; }

    private readonly List<Student> _students = new List<Student>();
    private readonly List<Teacher> _teachers = new List<Teacher>();
    
    public IReadOnlyList<Student> Students => _students;
    public IReadOnlyList<Teacher> Teachers => _teachers;
    
    protected Course(string title,  string description)
    {
        Title = title;
        Description = description;
    }

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public void AddTeacher(Teacher teacher)
    {
        _teachers.Add(teacher);
    }

    public abstract string GetCourseDetails();
}