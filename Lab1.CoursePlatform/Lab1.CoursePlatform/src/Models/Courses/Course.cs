using System.Text;
using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Models.Courses;

public abstract class Course
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Description { get; set; } = string.Empty;

    private readonly List<Student> _students = new List<Student>();
    private readonly List<Teacher> _teachers = new List<Teacher>();
    
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<Teacher> Teachers => _teachers.AsReadOnly();
    
    protected Course(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Name cannot be null or white space.", nameof(title));
        Title = title;
    }

    public void AddStudent(Student student)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));
        if (_students.All(s => s.Id != student.Id)) _students.Add(student);
    }

    public void DeleteStudent(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        _students.Remove(_students.FirstOrDefault(s => s.Id == id));
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null) throw new ArgumentNullException(nameof(teacher));
        if (_teachers.All(s => s.Id != teacher.Id)) _teachers.Add(teacher);
    }
    
    public void DeleteTeacher(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        _teachers.Remove(_teachers.FirstOrDefault(t => t.Id == id));
    }

    public abstract string GetCourseDetails();
    
    protected void AppendInfoIfNotEmpty(StringBuilder sb, string label, string info)
    {
        if (!string.IsNullOrWhiteSpace(info))
        {
            sb.AppendLine($"{label}: {info}\n");
        }
    }
}