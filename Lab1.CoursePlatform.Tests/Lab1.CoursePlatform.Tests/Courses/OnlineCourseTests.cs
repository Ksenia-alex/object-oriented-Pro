using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;

using Xunit;

namespace Lab1.CoursePlatform.Tests.Courses;

public class OnlineCourseTests
{
    private readonly OnlineCourse _onlineCourse;
    private readonly Student _student1;
    private readonly Teacher _teacher1;

    public OnlineCourseTests()
    {
        _onlineCourse = new OnlineCourse(title: "Test Online Course", courseUrl: "TestOnlineCourse");
        _student1 = new Student(fullName: "Test Student1");
        _teacher1 = new Teacher(fullName: "Test Teacher1", specialization: "Test Specialization");
    }

    [Fact]
    public void AddStudent_ShouldAddStudent()
    {
        _onlineCourse.AddStudent(_student1);
        Assert.True(_onlineCourse.Students.Any());
        
        _onlineCourse.AddStudent(_student1);
        Assert.Single(_onlineCourse.Students);
    }
    
    [Fact]
    public void DeleteStudent_ShouldDeleteStudent()
    {
        _onlineCourse.AddStudent(_student1);
        _onlineCourse.DeleteStudent(_student1.Id);
        
        Assert.False(_onlineCourse.Students.Any());
        Assert.Empty(_onlineCourse.Students);
    }
    
    [Fact]
    public void AddTeacher_ShouldAddTeacher()
    {
        _onlineCourse.AddTeacher(_teacher1);
        Assert.True(_onlineCourse.Teachers.Any());
        
        _onlineCourse.AddTeacher(_teacher1);
        Assert.Single(_onlineCourse.Teachers);
    }
    
    [Fact]
    public void DeleteTeacher_ShouldDeleteTeacher()
    {
        _onlineCourse.AddTeacher(_teacher1);
        _onlineCourse.DeleteTeacher(_teacher1.Id);
        
        Assert.False(_onlineCourse.Teachers.Any());
        Assert.Empty(_onlineCourse.Teachers);
    }
}