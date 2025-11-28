using Lab1.CoursePlatform.Models.Courses;
using Lab1.CoursePlatform.Models.Persons;
using Lab1.CoursePlatform.Repositories;
using Lab1.CoursePlatform.Services;

namespace Lab1.CoursePlatform.App;

public class ConsoleApplication
{
    private static readonly StudentRepository StudentRepository = new();
    private static readonly TeacherRepository TeacherRepository = new();
    private static readonly CourseRepository CourseRepository = new();
    private readonly CourseControlService _controlService = new(CourseRepository, StudentRepository, TeacherRepository);

    public void Run()
    {
        while (true)
        {
            var mainCommands = new Dictionary<string, Command>()
            {
                ["1"] = new("Посмотреть список курсов", ViewCourses),
                ["2"] = new("Посмотреть детали курса", ViewCourseDetails),
                ["3"] = new("Добавить новый курс", AddCourse),
                ["4"] = new("Удалить курс", DeleteCourse),
                ["5"] = new("Добавить студента в систему", AddStudent),
                ["6"] = new("Добавить преподователя в систему", AddTeacher),
                ["7"] = new("Записать студента на курс", RegisterStudent),
                ["8"] = new("Отчислить студента с курса", DeleteStudent),
                ["9"] = new("Назначить преподавателя на курс", RegisterTeacher),
                ["10"] = new("Снять преподавателя с курса", DeleteTeacher),
                ["11"] = new("Показать все курсы преподавателя", ViewCoursesForTeacher),
                ["12"] = new("Посмотреть всю информацию о студенте курса", ViewStudentDetailsOnCourse),
                ["13"] = new("Заполнить тестовые данные", DataSeed),
                ["0"] = new("Выход", () => Environment.Exit(0))
            };

            ShowMessage("--- Команды ---", ConsoleColor.DarkBlue);
            foreach (var cmd in mainCommands)
            {
                Console.WriteLine($"{cmd.Key}. {cmd.Value.Name}");
            }
            
            Console.Write("\nВыберите действие: ");
            string? choice = Console.ReadLine();
            
            if (choice != null && mainCommands.TryGetValue(choice, out var command))
            {
                command.Action.Invoke();
            }
            else
            {
                ShowMessage("Неверная команда", ConsoleColor.Red);
            }
        }
    }
    
    private void ViewCourses()
    {
        ShowMessage("--- Курсы ---", ConsoleColor.DarkBlue);
        var allCourses = _controlService.GetAllCourses().ToList();
        if (!allCourses.Any())
        {
            Console.WriteLine("На платформе пока нет курсов:(");
            return;
        }
        
        for (int i = 0; i < allCourses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {allCourses[i].Title}");
        }
    }
    
    private void ViewCourseDetails()
    {
        ShowMessage("--- Детальная информация о курсе ---", ConsoleColor.DarkBlue);
        Course? course = SelectCourse("Введите ID курса:"); 
        if (course != null)
        {
            Console.WriteLine(course.GetCourseDetails());
        }
        else
        {
            ShowMessage("Курс с таким ID не найден.", ConsoleColor.Red);
        }
    }

    private void AddCourse()
    {
        ShowMessage("--- Добавление нового курса ---", ConsoleColor.DarkBlue);
        string courseTypeChoice = PromptForInput("Выберите тип курса (1 - Онлайн, 2 - Офлайн):", isRequired: true);
        
        string title = PromptForInput("Введите название курса (обязательно):", isRequired: true);
        string description = PromptForInput("Введите описание (необязательно):");
        
        Course? newCourse = null;
        
        switch (courseTypeChoice)
        {
            case "1":
                string url = PromptForInput("Введите URL курса (обязательно):", isRequired: true);
                newCourse = new OnlineCourse(title, url) { Description = description };
                break;
            case "2":
                string city = PromptForInput("Введите город (необязательно):");
                string address = PromptForInput("Введите адрес (необязательно):");
                string lectureRoom = PromptForInput("Введите аудиторию (необязательно):");
                newCourse = new OfflineCourse(title)
                {
                    City = city, 
                    Address = address, 
                    LectureRoom = lectureRoom,
                    Description = description
                };
                break;
            default:
                ShowMessage("Неверный тип курса!", ConsoleColor.Red);
                break;
            
        }
        
        if (newCourse != null)
        {
            _controlService.AddCourse(newCourse);
            ShowMessage($"\nКурс '{newCourse.Title}' успешно добавлен!", ConsoleColor.Green);
        }
    }
    
    private void DeleteCourse()
    {
        ShowMessage("--- Удаление курса ---", ConsoleColor.DarkBlue);
        Course? course = SelectCourse("Введите ID курса:");
        if (course == null) return;

        _controlService.DeleteCourse(course.Id);
        ShowMessage("Курс успешно удален.", ConsoleColor.Green);
    }
    
    private void AddStudent()
    {
        ShowMessage("--- Добавление нового студента ---", ConsoleColor.DarkBlue);
        string fullName = PromptForInput("Введите ФИО студента:", isRequired: true);
        string personalInfo = PromptForInput("Введите доп. информацию (необязательно):");
        string email = PromptForInput("Введите почту (необязательно):");
        string phoneNumber = PromptForInput("Введите номер телефона (необязательно):");
        string birthDate = PromptForInput("Введите дату рождения (необязательно):");
    
        var newStudent = new Student(fullName, personalInfo)
        {
            Email = email,
            PhoneNumber = phoneNumber,
            BirthDate = DateTime.Parse(birthDate)
        };
        StudentRepository.Add(newStudent);
    
        ShowMessage($"Студент '{fullName}' успешно добавлен.", ConsoleColor.Green);
    }

    private void AddTeacher()
    {
        ShowMessage("--- Добавление нового преподавателя ---", ConsoleColor.DarkBlue);
        string fullName = PromptForInput("Введите ФИО преподавателя (обязательно):", isRequired: true);
        string specialization = PromptForInput("Введите специализацию (обязательно):", isRequired: true);
        string email = PromptForInput("Введите почту (необязательно):");
        string phoneNumber = PromptForInput("Введите номер телефона (необязательно):");
        string birthDate = PromptForInput("Введите дату рождения (необязательно):");
        string bio = PromptForInput("Введите биографию (необязательно):");

        var newTeacher = new Teacher(fullName, specialization)
        {
            Bio = bio,
            Email = email,
            PhoneNumber = phoneNumber,
            BirthDate = DateTime.Parse(birthDate)
        };
        TeacherRepository.Add(newTeacher);
    
        ShowMessage($"Преподаватель '{fullName}' успешно добавлен.", ConsoleColor.Green);
    }
    
    private void RegisterStudent()
    {
        ShowMessage("--- Запись студента на курс ---", ConsoleColor.DarkBlue);
        Course? course = SelectCourse("Введите ID курса:");
        if (course == null) return;
        Student? student = SelectStudent("Введите ID студента:");
        if (student == null) return;
        
        try
        {
            _controlService.RegisterStudent(course.Id, student.Id);
            ShowMessage("Студент успешно записан на курс.", ConsoleColor.Green);
        }
        catch (ArgumentException)
        {
            ShowMessage("Ошибка: курс или студент с указанными ID не найдены.", ConsoleColor.Red);
        }
    }

    private void DeleteStudent()
    {
        Console.Clear();
        ShowMessage("--- Удаление студента из системы ---", ConsoleColor.DarkBlue);
        var studentToDelete = SelectStudent("Введите номер студента для удаления:");

        if (studentToDelete != null)
        {
            StudentRepository.Delete(studentToDelete.Id);
            ShowMessage($"Студент '{studentToDelete.FullName}' успешно удален.", ConsoleColor.Green);
        }
    }

    private void RegisterTeacher()
    {
        ShowMessage("--- Назначение преподавателя на курс ---", ConsoleColor.DarkBlue);
        Course? course = SelectCourse("Введите ID курса:");
        if (course == null) return;
        Teacher? teacher = SelectTeacher("Введите ID преподавателя:");
        if (teacher == null) return;

        try
        {
            _controlService.RegisterTeacher(course.Id, teacher.Id);
            ShowMessage("Преподаватель успешно назначен.", ConsoleColor.Green);
        }
        catch (ArgumentException)
        {
            ShowMessage("Ошибка: курс или преподаватель с указанными ID не найдены.", ConsoleColor.Red);
        }
    }

    private void DeleteTeacher()
    {
        ShowMessage("--- Снятие преподавателя с курса ---", ConsoleColor.DarkBlue);
        var course = SelectCourse("Выберите курс, с которого нужно снять преподавателя:");
        if (course == null) return;
        
        var teachersOnCourse = course.Teachers.ToList();
        if (!teachersOnCourse.Any())
        {
            ShowMessage("На этом курсе нет назначенных преподавателей.", ConsoleColor.Yellow);
            return;
        }

        Console.WriteLine("\nПреподаватели на этом курсе:");
        for (int i = 0; i < teachersOnCourse.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {teachersOnCourse[i].FullName}");
        }
        
        string choice = PromptForInput("\nВведите номер преподавателя для снятия с курса:", true);
        if (int.TryParse(choice, out int index) && index > 0 && index <= teachersOnCourse.Count)
        {
            var teacherToRemove = teachersOnCourse[index - 1];
            
            _controlService.RemoveTeacher(course.Id, teacherToRemove.Id);
        
            ShowMessage("Преподаватель успешно снят с курса.", ConsoleColor.Green);
        }
        else
        {
            ShowMessage("Неверный номер.", ConsoleColor.Red);
        }
    }

    private void ViewCoursesForTeacher()
    {
        ShowMessage("--- Поиск курсов по преподавателю ---", ConsoleColor.DarkBlue);
        Teacher? teacher = SelectTeacher("Введите ID преподавателя:");
        if (teacher == null) return;

        var courses = _controlService.GetCoursesForTeacher(teacher.Id);
        
        Console.WriteLine($"\nКурсы, которые ведет {teacher?.FullName}:");
        if (!courses.Any())
        {
            Console.WriteLine("Этот преподаватель пока не ведет курсов.");
            return;
        }

        foreach (var course in courses)
        {
            Console.WriteLine($"- {course.Title}");
        }
    }
    
    private void ViewStudentDetailsOnCourse()
    {
        ShowMessage("--- Просмотр информации о студентах на курсе ---", ConsoleColor.DarkBlue);
        
        var selectedCourse = SelectCourse("Введите ID курса:");
        if (selectedCourse == null)
        {
            return;
        }
        var selectedStudent = SelectStudent("Введите номер студента:");
        if (selectedStudent == null)
        {
            return;
        }
        Console.WriteLine($"--- Полная информация о студенте: {selectedStudent.FullName} ---");
        Console.WriteLine(selectedStudent.GetFullInfo());
    }
    
    private string PromptForInput(string prompt, bool isRequired = false)
    {
        while (true)
        {
            Console.Write($"{prompt} ");
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            
            if (isRequired)
            {
                ShowMessage("Это поле обязательно для заполнения. Пожалуйста, введите значение.", ConsoleColor.Yellow);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    
    private Course? SelectCourse(string prompt)
    {
        ViewCourses(); 
        var courses = _controlService.GetAllCourses().ToList();
        if (!courses.Any()) return null;
        
        string choice = PromptForInput($"\n{prompt}", true);

        if (int.TryParse(choice, out int index) && index > 0 && index <= courses.Count)
        {
            return courses[index - 1];
        }

        ShowMessage("Неверный номер курса.", ConsoleColor.Red);
        return null;
    }
    
    private Student? SelectStudent(string prompt)
    {
        Console.WriteLine("Доступные студенты:");
        var students = StudentRepository.GetAll().ToList();
        if (!students.Any())
        {
            ShowMessage("В системе нет зарегистрированных студентов.", ConsoleColor.Yellow);
            return null;
        }
    
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i].FullName}");
        }

        string choice = PromptForInput($"\n{prompt}", true);
        if (int.TryParse(choice, out int index) && index > 0 && index <= students.Count)
        {
            return students[index - 1];
        }
    
        ShowMessage("Неверный номер студента.", ConsoleColor.Red);
        return null;
    }

    private Teacher? SelectTeacher(string prompt)
    {
        Console.WriteLine("Доступные преподаватели:");
        var teachers = TeacherRepository.GetAll().ToList();
        if (!teachers.Any())
        {
            ShowMessage("В системе нет зарегистрированных преподавателей.", ConsoleColor.Yellow);
            return null;
        }
    
        for (int i = 0; i < teachers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {teachers[i].FullName}");
        }
    
        string choice = PromptForInput($"\n{prompt}", true);
        if (int.TryParse(choice, out int index) && index > 0 && index <= teachers.Count)
        {
            return teachers[index - 1];
        }
    
        ShowMessage("Неверный номер преподавателя.", ConsoleColor.Red);
        return null;
    }
    
    private void ShowMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private void DataSeed()
    {
        ShowMessage("--- Заполнение базы тестовыми данными... ---", ConsoleColor.DarkBlue);
        
        Data.DatabaseSeeder.Seed(StudentRepository, TeacherRepository, CourseRepository);
        ShowMessage("Тестовые данные успешно загружены!", ConsoleColor.Green);
    }
}