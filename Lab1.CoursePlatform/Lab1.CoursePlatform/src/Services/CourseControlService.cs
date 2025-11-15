using Lab1.CoursePlatform.Interfaces;
using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Services;

public class CourseControlService : InterfaceCourseControlService
{
    private readonly List<Course> _courses = new List<Course>();
    private readonly List<Student> _students = new List<Student>();
    private readonly List<Teacher> _teachers = new List<Teacher>();

    public void AddCourse(Course course)
    {

    }

    public void DeleteCourse(string courseId)
    {

    }

    public void UpdateCourse(string courseId)
    {
        
    }

    public void RegisterStudent(string courseId, string studentId)
    {
        
    }

    public void RemoveStudent(string courseId, string studentId)
    {
        
    }
    
    public void RegisterTeacher(string courseId, string teacherId)
    {
        
    }
    
    public void RemoveTeacher(string courseId, string teacherId)
    {
        
    }
    
    public IEnumerable<Course> GetCoursesForStudent(string studentId)
    {
        return _courses.Where(c => c.Students.Any(i => i.Id == studentId));
    }
    
    public IEnumerable<Course> GetCoursesForTeacher(string teacherId)
    {
        return _courses.Where(c => c.Teachers.Any(i => i.Id == teacherId));
    }
    
    public Course GetCourse(string courseId)
    {
        return  _courses.FirstOrDefault(c => c.Id == courseId);
    }
}