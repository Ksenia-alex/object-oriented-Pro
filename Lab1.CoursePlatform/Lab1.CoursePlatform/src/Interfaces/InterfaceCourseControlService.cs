using Lab1.CoursePlatform.Models.Courses;

namespace Lab1.CoursePlatform.Interfaces;

public interface InterfaceCourseControlService
{
    void AddCourse(Course course);
    void DeleteCourse(string courseId);
    void RegisterStudent(string courseId, string studentId);
    void RemoveStudent(string courseId, string studentId);
    void RegisterTeacher(string courseId, string teacherId);
    void RemoveTeacher(string courseId, string teacherId);
    IEnumerable<Course> GetCoursesForTeacher(string teacherId);
    Course GetCourse(string courseId);
}