namespace Lab1.CoursePlatform.Interfaces;

public interface InterfaceRepository<T> where T : class
{
    T? GetById(string id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Delete(string id);
    void Update(T entity);
}