using Lab1.CoursePlatform.Models.Persons;
using Lab1.CoursePlatform.Repositories;
using Xunit;

namespace Lab1.CoursePlatform.Tests.Repositories;

public class StudentRepositoryTests
{
    private readonly StudentRepository _repository;
    private readonly Student _student1;
    private readonly Student _student2;
    
    public StudentRepositoryTests()
    {
        _repository = new StudentRepository();
        _student1 = new Student("Test Student1");
        _student2 = new Student("Test Student2");
    }
    
    [Fact]
    public void Add_ShouldAddStudent()
    {
        _repository.Add(_student1);

        Assert.Single(_repository.GetAll());
        
        string student1Id = _student1.Id;
        Assert.Equal($"ФИО: Test Student1", _student1.GetFullInfo());
    }
    
    [Fact]
    public void Delete_ShouldDeleteStudent()
    {
        _repository.Add(_student1);
        
        _repository.Delete(_student1.Id);
        
        Assert.Empty(_repository.GetAll());
    }
    
    [Fact]
    public void GetById_ShouldReturnStudent()
    {
        _repository.Add(_student1);
        
        Assert.Equal(_student1, _repository.GetById(_student1.Id));
    }
    
    [Fact]
    public void GetAll_ShouldReturnAllStudents()
    {
        _repository.Add(_student1);
        _repository.Add(_student1);
        Assert.Single(_repository.GetAll());
        
        _repository.Add(_student2);
        Assert.Equal(2, _repository.GetAll().Count());
    }
    
}