using Lab1.CoursePlatform.Interfaces;
using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;

namespace Lab1.CoursePlatform.Data;

public static class DatabaseSeeder
{
    public static void Seed(
        InterfaceRepository<Student> studentRepo,
        InterfaceRepository<Teacher> teacherRepo,
        InterfaceRepository<Course> courseRepo)
    {
        var studentAlex = new Student("Алексей Новиков")
        {
            Email = "alex.n@email.com",
            PhoneNumber = "+79112223344",
            BirthDate = new DateTime(2002, 5, 15),
            PersonalInfo = "Студент 3-го курса, интересуется backend-разработкой."
        };

        var studentOlga = new Student("Ольга Захарова")
        {
            Email = "olga.z@email.com",
            BirthDate = new DateTime(2003, 11, 21),
            PersonalInfo = "Начинающий UI/UX дизайнер."
        };
        
        var studentDmitry = new Student("Дмитрий Ковалев");

        studentRepo.Add(studentAlex);
        studentRepo.Add(studentOlga);
        studentRepo.Add(studentDmitry);
        
        var teacherAnna = new Teacher("Анна Владимировна Смирнова", "Веб-разработка")
        {
            Bio = "Senior Frontend Developer с 10-летним опытом. Специализируется на React и Vue.",
            Email = "anna.smirnova@teachers.com"
        };
        
        var teacherIvan = new Teacher("Иван Игоревич Соколов", "Data Science")
        {
            Bio = "Кандидат технических наук, эксперт по машинному обучению и Python.",
            PhoneNumber = "+79219876543"
        };
        
        teacherRepo.Add(teacherAnna);
        teacherRepo.Add(teacherIvan);
        
        var frontendCourse = new OnlineCourse("Современный Frontend", "http://courses/frontend-pro")
        {
            Description = "Полный курс по разработке веб-интерфейсов на React."
        };
        frontendCourse.AddTeacher(teacherAnna); 
        frontendCourse.AddStudent(studentAlex);
        frontendCourse.AddStudent(studentOlga);

        var dataScienceCourse = new OfflineCourse("Основы Data Science")
        {
            Description = "Практический курс для начинающих аналитиков данных.",
            Address = "ул. Программистов, д. 1, ауд. 101"
        };
        dataScienceCourse.AddTeacher(teacherIvan);
        dataScienceCourse.AddStudent(studentAlex);
        dataScienceCourse.AddStudent(studentDmitry);

        var emptyCourse = new OnlineCourse("Курс в разработке", "http://courses/soon");

        courseRepo.Add(frontendCourse);
        courseRepo.Add(dataScienceCourse);
        courseRepo.Add(emptyCourse);
    }
}