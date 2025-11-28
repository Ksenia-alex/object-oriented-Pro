using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Repositories;
using Xunit;

namespace Lab1.CoursePlatform.Tests.Repositories;

public class CourseRepositoryTests
{
    private readonly CourseRepository _repository;
    private readonly OnlineCourse _onlineCourse;
    private readonly OfflineCourse _offlineCourse;
    
    public CourseRepositoryTests()
    {
        _repository = new CourseRepository();
        _onlineCourse = new OnlineCourse(title: "Test OnlineCourse", courseUrl: "TestOnlineCourse");
        _offlineCourse = new OfflineCourse(title: "Test OfflineCourse");
    }
    
    [Fact]
    public void Add_ShouldAddTeacher()
    {
        _repository.Add(_onlineCourse);

        Assert.Single(_repository.GetAll());
    }
    
    [Fact]
    public void Delete_ShouldDeleteTeacher()
    {
        _repository.Add(_onlineCourse);
        
        _repository.Delete(_onlineCourse.Id);
        
        Assert.Empty(_repository.GetAll());
    }
    
    [Fact]
    public void GetById_ShouldReturnTeacher()
    {
        _repository.Add(_onlineCourse);
        
        Assert.Equal(_onlineCourse, _repository.GetById(_onlineCourse.Id));
    }
    
    [Fact]
    public void GetAll_ShouldReturnAllTeacher()
    {
        _repository.Add(_onlineCourse);
        _repository.Add(_onlineCourse);
        Assert.Single(_repository.GetAll());
        
        _repository.Add(_offlineCourse);
        Assert.Equal(2, _repository.GetAll().Count());
    }
    
    [Fact]
    public void GetAllOnlineCourses_ShouldRetutnOnlyOnlineCourses()
    {
        _repository.Add(_onlineCourse);
        _repository.Add(_offlineCourse);
        
        Assert.Single(_repository.GetAllOnlineCourses());
    }

    [Fact]
    public void GetAllOfflineCourses_ShouldRetutnOnlyOfflineCourses()
    {
        _repository.Add(_onlineCourse);
        _repository.Add(_offlineCourse);
        _repository.Add(_offlineCourse);
        Assert.Single(_repository.GetAllOfflineCourses());
    }
}