using Lab1.CoursePlatform.Interfaces;
using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Services;

public class CourseControlService : InterfaceCourseControlService
{
    private readonly InterfaceRepository<Course> _courseRepository;
    private readonly InterfaceRepository<Student> _studentRepository;
    private readonly InterfaceRepository<Teacher> _teacherRepository;

    public CourseControlService(
        InterfaceRepository<Course> courseRepository,
        InterfaceRepository<Student> studentRepository,
        InterfaceRepository<Teacher> teacherRepository)
    {
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }

    public void AddCourse(Course course)
    {
        if (course is null) throw new ArgumentNullException(nameof(course));
        _courseRepository.Add(course);
    }

    public void DeleteCourse(string courseId)
    {
        if (string.IsNullOrWhiteSpace(courseId)) return;
        _courseRepository.Delete(courseId);
    }

    public void RegisterStudent(string courseId, string studentId)
    {
        var course = _courseRepository.GetById(courseId);
        var student = _studentRepository.GetById(studentId);
        
        if (course is null) throw new ArgumentException("Курс не найден", nameof(course));
        if (student is null) throw new ArgumentException("Студент не найден", nameof(student));
        
        course.AddStudent(student);
        _courseRepository.Update(course);
    }

    public void RemoveStudent(string courseId, string studentId)
    {
        var  course = _courseRepository.GetById(courseId);
        if (course is null) return;
        course.DeleteStudent(studentId);
        _courseRepository.Update(course);
    }
    
    public void RegisterTeacher(string courseId, string teacherId)
    {
        var course = _courseRepository.GetById(courseId);
        var teacher = _teacherRepository.GetById(teacherId);
        
        if (course is null) throw new ArgumentException("Курс не найден", nameof(course));
        if (teacher is null) throw new ArgumentException("Преподаватель не найден", nameof(teacher));
        
        course.AddTeacher(teacher);
        _courseRepository.Update(course);
    }
    
    public void RemoveTeacher(string courseId, string teacherId)
    {
        var  course = _courseRepository.GetById(courseId);
        if (course is null) return;
        course.DeleteTeacher(teacherId);
        _courseRepository.Update(course);
    }
    
    public IEnumerable<Course> GetCoursesForTeacher(string teacherId)
    {
        return _courseRepository.GetAll().Where(c => c.Teachers.Any(i => i.Id == teacherId));
    }
    
    public Course? GetCourse(string courseId)
    {
        return _courseRepository.GetById(courseId);
    }
    
    public IEnumerable<Course> GetAllCourses()
    {
        return _courseRepository.GetAll();
    }
}