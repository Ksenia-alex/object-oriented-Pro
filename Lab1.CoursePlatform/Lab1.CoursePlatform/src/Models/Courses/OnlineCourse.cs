namespace Lab1.CoursePlatform.Models.Courses;

public class OnlineCourse : Course
{
    public string CourseUrl { get; set; }

    public OnlineCourse(string title, string courseUrl, string description)
        : base(title, description)
    {
        CourseUrl = courseUrl;
    }
    
    public override string GetCourseDetails()
    {
        return $"Тип: Онлайн\nОписание: {Description}\nURL: {CourseUrl}";
    }
}