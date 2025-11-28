using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Repositories;

public class TeacherRepository() : Repository<Teacher>(teacher => teacher.Id);