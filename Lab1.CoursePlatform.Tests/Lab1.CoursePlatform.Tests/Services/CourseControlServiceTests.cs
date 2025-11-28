using Lab1.CoursePlatform.Interfaces;
using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;
using Lab1.CoursePlatform.Repositories;
using Lab1.CoursePlatform.Services;

namespace Lab1.CoursePlatform.Tests.Services;

public class CourseControlServiceTests
{
    private readonly InterfaceRepository<Course> _courseRepository;
    private readonly InterfaceRepository<Student> _studentRepository;
    private readonly InterfaceRepository<Teacher> _teacherRepository;
    private readonly CourseControlService _service;

    private readonly Student _student;
    private readonly Teacher _teacher;
    private readonly Course _course;

    public CourseControlServiceTests()
    {
        _studentRepository = new StudentRepository();
        _teacherRepository = new TeacherRepository();
        _courseRepository = new CourseRepository();
        
        _service = new CourseControlService(_courseRepository, _studentRepository, _teacherRepository);
        
        _student = new Student("Test Student");
        _teacher = new Teacher("Test Teacher1", "Test Specialization");
        _course = new OnlineCourse(title: "Test OnlineCourse", courseUrl: "TestOnlineCourse");
    }
    
    [Fact]
    public void AddCourse_ShouldAddCourseToRepository()
    {
        _service.AddCourse(_course);
        
        Assert.Single(_courseRepository.GetAll());
        Assert.Equal(_course.Id, _courseRepository.GetById(_course.Id)?.Id);
    }

    [Fact]
    public void DeleteCourse_ShouldRemoveCourseFromRepository()
    {
        _courseRepository.Add(_course);
        
        Assert.Single(_courseRepository.GetAll());
        
        _service.DeleteCourse(_course.Id);
        
        Assert.Empty(_courseRepository.GetAll());
    }
    
    [Fact] //
    public void UpdateCourse_ShouldChangeCourseDataInRepository()
    {
        _courseRepository.Add(_course);
        _course.Title = "Новое Название Курса";
        
        _courseRepository.Update(_course);
        
        var updatedCourse = _courseRepository.GetById(_course.Id);
        Assert.Equal("Новое Название Курса", updatedCourse?.Title);
    }

    [Fact]
    public void RegisterStudent_ShouldAddStudentToCourse()
    {
        _studentRepository.Add(_student);
        _courseRepository.Add(_course);
        
        _service.RegisterStudent(_course.Id, _student.Id);
        
        var resultCourse = _courseRepository.GetById(_course.Id);
        Assert.Single(resultCourse.Students.ToList());
        Assert.Equal(_student.Id, resultCourse.Students.First().Id);
    }
    
    [Fact]
    public void RemoveStudent_ShouldRemoveStudentFromCourse()
    {
        _studentRepository.Add(_student);
        _course.AddStudent(_student);
        _courseRepository.Add(_course);
        
        _service.RemoveStudent(_course.Id, _student.Id);
        
        var resultCourse = _courseRepository.GetById(_course.Id);
        Assert.Empty(resultCourse.Students.ToList());
    }
    
    [Fact]
    public void RegisterTeacher_ShouldAssignTeacherToCourse()
    {
        _teacherRepository.Add(_teacher);
        _courseRepository.Add(_course);
        
        _service.RegisterTeacher(_course.Id, _teacher.Id);
        
        var resultCourse = _courseRepository.GetById(_course.Id);
        Assert.Single(resultCourse.Teachers);
        Assert.Equal(_teacher.Id, resultCourse.Teachers.First().Id);
    }
    
    [Fact]
    public void RemoveTeacher_ShouldRemoveTeacherFromCourse()
    {
        _teacherRepository.Add(_teacher);
        _course.AddTeacher(_teacher);
        _courseRepository.Add(_course);
        
        _service.RemoveTeacher(_course.Id, _teacher.Id);
        
        var resultCourse = _courseRepository.GetById(_course.Id);
        Assert.Empty(resultCourse.Teachers.ToList());
    }
    
    [Fact]
    public void GetCoursesForTeacher_ShouldReturnOnlyTheirCourses()
    {
        var anotherTeacher = new Teacher("Test teacher2", "Test Specialization");
        _teacherRepository.Add(_teacher);
        _teacherRepository.Add(anotherTeacher);

        var anotherCourse = new OfflineCourse("Test Offline Course");
        
        _course.AddTeacher(_teacher);
        anotherCourse.AddTeacher(anotherTeacher);

        _courseRepository.Add(_course);
        _courseRepository.Add(anotherCourse);
        
        var result = _service.GetCoursesForTeacher(_teacher.Id).ToList();

        Assert.Single(result);
        Assert.Equal(_course.Id, result.First().Id);
    }
    
    [Fact]
    public void GetCourse_ShouldReturnCorrectCourse()
    {
        _courseRepository.Add(_course);
        
        var result = _service.GetCourse(_course.Id);
        
        Assert.NotNull(result);
        Assert.Equal(_course.Id, result.Id);
    }
    
    [Fact]
    public void GetAllCourses_ShouldReturnAllCoursesFromRepository()
    {
        var anotherCourse = new OfflineCourse("Test Offline Course");
        _courseRepository.Add(_course);
        _courseRepository.Add(anotherCourse);
        
        var result = _service.GetAllCourses().ToList();
        
        Assert.Equal(2, result.Count);
        Assert.Contains(_course, result);
        Assert.Contains(anotherCourse, result);
    }
}