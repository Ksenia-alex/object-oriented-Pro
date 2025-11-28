using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Repositories;

public class StudentRepository() : Repository<Student>(student => student.Id);