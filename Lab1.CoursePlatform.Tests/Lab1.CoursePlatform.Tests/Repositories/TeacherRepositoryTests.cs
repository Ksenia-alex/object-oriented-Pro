using Lab1.CoursePlatform.Models.Persons;
using Lab1.CoursePlatform.Repositories;
using Xunit;

namespace Lab1.CoursePlatform.Tests.Repositories;

public class TeacherRepositoryTests
{
    private readonly TeacherRepository _repository;
    private readonly Teacher _teacher1;
    private readonly Teacher _teacher2;
    
    public TeacherRepositoryTests()
    {
        _repository = new TeacherRepository();
        _teacher1 = new Teacher("Test Teacher1", "Test Specialization");
        _teacher2 = new Teacher("Test Teacher2", "Test Specialization");
    }
    
    [Fact]
    public void Add_ShouldAddTeacher()
    {
        _repository.Add(_teacher1);

        Assert.Single(_repository.GetAll());
        Assert.Equal($"ФИО: Test Teacher1\nСпециальность: Test Specialization", _teacher1.GetFullInfo());
    }
    
    [Fact]
    public void Delete_ShouldDeleteTeacher()
    {
        _repository.Add(_teacher1);
        
        _repository.Delete(_teacher1.Id);
        
        Assert.Empty(_repository.GetAll());
    }
    
    [Fact]
    public void GetById_ShouldReturnTeacher()
    {
        _repository.Add(_teacher1);
        
        Assert.Equal(_teacher1, _repository.GetById(_teacher1.Id));
    }
    
    [Fact]
    public void GetAll_ShouldReturnAllTeacher()
    {
        _repository.Add(_teacher1);
        _repository.Add(_teacher1);
        Assert.Single(_repository.GetAll());
        
        _repository.Add(_teacher2);
        Assert.Equal(2, _repository.GetAll().Count());
    }
    
}