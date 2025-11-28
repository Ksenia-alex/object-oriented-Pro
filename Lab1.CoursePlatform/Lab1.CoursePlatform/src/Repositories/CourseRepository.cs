using Lab1.CoursePlatform.Models.Courses;

namespace Lab1.CoursePlatform.Repositories;

public class CourseRepository() : Repository<Course>(course => course.Id)
{
    public IEnumerable<OnlineCourse> GetAllOnlineCourses()
    {
        return _concurrentDictionary.Values.OfType<OnlineCourse>();
    }

    public IEnumerable<OfflineCourse> GetAllOfflineCourses()
    {
        return _concurrentDictionary.Values.OfType<OfflineCourse>();
    } 
}