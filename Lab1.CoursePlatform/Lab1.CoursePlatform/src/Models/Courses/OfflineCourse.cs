namespace Lab1.CoursePlatform.Models.Courses;

public class OfflineCourse : Course
{
    public string City { get; set; }
    public string Address { get; set; }
    public string LectureRoom {  get; set; }

    public OfflineCourse(string title, string description, string city, string address, string lectureRoom)
        : base(title, description)
    {
        City = city;
        Address = address;
        LectureRoom = lectureRoom;
    }

    public override string GetCourseDetails()
    {
        return  $"Тип: Оффлайн\nОписание: {Description}\nГород: {City}\nАдрес: {Address}\nАудитория: {LectureRoom}";
    }
}