using System.Text;

namespace Lab1.CoursePlatform.Models.Courses;

public class OfflineCourse : Course
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string LectureRoom { get; set; } = string.Empty;

    public OfflineCourse(string title)
        : base(title)
    {
    }

    public override string GetCourseDetails()
    {
        var sb = new StringBuilder();
        sb.AppendLine("--- Детали курса ---");
        sb.Append($"Название: {Title}");
        AppendInfoIfNotEmpty(sb, "Описание",Description);
        AppendInfoIfNotEmpty(sb,"Город",City);
        AppendInfoIfNotEmpty(sb,"Адресс",Address);
        AppendInfoIfNotEmpty(sb,"Аудитория",LectureRoom);
        sb.AppendLine();
        sb.AppendLine("Преподаватели:");
        if (Teachers.Any())
        {
            foreach (var  teacher in Teachers)
            {
                sb.AppendLine($"- {teacher.FullName} ({teacher.Specialization})");
            }
        }
        else sb.AppendLine("Преподаватели еще не назначены.");
        sb.AppendLine();
        sb.AppendLine("Cтуденты:");
        if (Students.Any())
        {
            foreach (var student in Students)
            {
                sb.AppendLine($"- {student.FullName}");
            }
        }
        else sb.AppendLine("На курс пока никто не записан.");
        
        return sb.ToString().Trim();
    }
}